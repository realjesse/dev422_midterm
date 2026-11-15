using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Login;

[ApiController]
[AllowAnonymous]
[Route("api/users/login")]
public class Endpoint : ControllerBase
{
    private readonly Login _login;

    public Endpoint(Login login)
    {
        _login = login;
    }

    [HttpPost]
    public async Task<ActionResult<Response>> Handle([FromBody] Request request)
    {
        try
        {
            var user = _login.Execute(request.Username, request.Password);
            if (user is null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                },
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return Ok(new Response(user.Id, user.Username));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
