using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Register;

[ApiController]
[AllowAnonymous]
[Route("api/users/register")]
public class Endpoint : ControllerBase
{
    private readonly Register _register;

    public Endpoint(Register register)
    {
        _register = register;
    }

    [HttpPost]
    public ActionResult<Response> Handle([FromBody] Request request)
    {
        try
        {
            var user = _register.Execute(request.Username, request.Password);
            return StatusCode(StatusCodes.Status201Created, new Response(user.Id, user.Username));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}
