using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;

[ApiController]
[Authorize]
[Route("api/playlists/{id:int}/invitations")]
public class Endpoint : ControllerBase
{
    private readonly InviteCollaborator _inviteCollaborator;

    public Endpoint(InviteCollaborator inviteCollaborator)
    {
        _inviteCollaborator = inviteCollaborator;
    }

    [HttpPost]
    public ActionResult<Response> Handle(int id, [FromBody] Request request)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId) || userId == Guid.Empty)
        {
            return Unauthorized();
        }

        try
        {
            var playlist = _inviteCollaborator.Execute(id, userId, request.Username);
            return Ok(new Response(playlist.Id, playlist.Name, playlist.Songs.ToList(),
                playlist.CreatedBy, playlist.IsCollaborative, playlist.Collaborators.ToList()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}
