using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;

[ApiController]
[Authorize]
[Route("api/playlists")]
public class Endpoint : ControllerBase
{
    private readonly CreatePlaylist _createPlaylist;

    public Endpoint(CreatePlaylist createPlaylist)
    {
        _createPlaylist = createPlaylist;
    }

    [HttpPost]
    public ActionResult<Response> Handle([FromBody] Request request)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId) || userId == Guid.Empty)
        {
            return Unauthorized();
        }

        try
        {
            var playlist = _createPlaylist.Execute(request.Name, userId, request.IsCollaborative);
            var response = new Response(playlist.Id, playlist.Name, playlist.Songs.ToList(),
                playlist.CreatedBy, playlist.IsCollaborative);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
