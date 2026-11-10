using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            var playlist = _createPlaylist.Execute(request.Name, request.CreatedBy, request.IsCollaborative);
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
