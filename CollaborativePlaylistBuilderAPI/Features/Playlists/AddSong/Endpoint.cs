using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

[ApiController]
[Authorize]
[Route("api/playlists/{id:int}/songs/{songId:int}")]
public class Endpoint : ControllerBase
{
    private readonly AddSong _addSong;

    public Endpoint(AddSong addSong)
    {
        _addSong = addSong;
    }

    [HttpPut]
    public ActionResult<Response> Handle([FromRoute] Request request)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId) || userId == Guid.Empty)
        {
            return Unauthorized();
        }

        try
        {
            var playlist = _addSong.Execute(request.Id, request.SongId, userId);
            return Ok(new Response(playlist.Id, playlist.Name, playlist.Songs.ToList(),
                playlist.CreatedBy, playlist.IsCollaborative, playlist.Collaborators.ToList()));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
    }
}
