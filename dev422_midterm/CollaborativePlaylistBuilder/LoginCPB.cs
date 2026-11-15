using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dev422_midterm.CollaborativePlaylistBuilder
{
    public partial class LoginCPB : Form
    {
        private readonly PlaylistApiClient api;
        private bool busy;

        public LoginCPB()
        {
            InitializeComponent();
            backButton.Click += (sender, args) => Close();
            registerButton.Click += async (sender, args) => await SubmitAsync(true);
            loginButton.Click += async (sender, args) => await SubmitAsync(false);
            FormClosing += (sender, args) => { if (busy) args.Cancel = true; };
        }

        internal LoginCPB(PlaylistApiClient api) : this()
        {
            this.api = api;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async Task SubmitAsync(bool register)
        {
            if (busy) return;
            busy = true;
            try
            {
                if (api == null)
                    throw new InvalidOperationException("Open Login from Form1 first.");

                var username = usernameTextBox.Text.Trim();
                var password = passwordTextBox.Text;
                if (register)
                {
                    await api.RegisterAsync(username, password);
                    MessageBox.Show(this, "Account created. Click Login to sign in.");
                }
                else
                {
                    await api.LoginAsync(username, password);
                    busy = false;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(this, "Cannot reach the playlist API. Make sure it is running.");
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(this, "The request timed out. Please try again.");
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(this, exception.Message);
            }
            finally
            {
                busy = false;
            }
        }
    }
}
