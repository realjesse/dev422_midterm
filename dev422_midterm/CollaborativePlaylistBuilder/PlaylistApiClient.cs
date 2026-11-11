using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    // Keep this client alive for the whole signed-in visit so requests share the session cookie.
    internal sealed class PlaylistApiClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer json = new JavaScriptSerializer();

        public PlaylistApiClient()
        {
            var address = ConfigurationManager.AppSettings["PlaylistApiBaseUrl"] ?? "http://localhost:5123/";
            client = new HttpClient(new HttpClientHandler { CookieContainer = new CookieContainer(), UseCookies = true })
            {
                BaseAddress = new Uri(address.TrimEnd('/') + "/"),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        public async Task RegisterAsync(string username, string password)
        {
            await SendAsync("api/users/register", username, password);
        }

        public Task<UserSession> LoginAsync(string username, string password)
        {
            return SendAsync("api/users/login", username, password);
        }

        private async Task<UserSession> SendAsync(string route, string username, string password)
        {
            using (var body = new StringContent(json.Serialize(new { username, password }), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(route, body))
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                        throw new InvalidOperationException("Invalid username or password.");
                    if (response.StatusCode == HttpStatusCode.Conflict)
                        throw new InvalidOperationException("That username is already registered. Try logging in instead.");
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                        throw new InvalidOperationException("Check your username and password and try again.");
                    throw new InvalidOperationException("The playlist service could not complete the request. Please try again.");
                }
                var user = json.Deserialize<UserSession>(content);
                if (user == null || user.Id == Guid.Empty || string.IsNullOrWhiteSpace(user.Username))
                    throw new InvalidOperationException("The playlist service returned an unexpected response.");
                return user;
            }
        }

        public void Dispose() { client.Dispose(); }
    }

    internal sealed class UserSession
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
