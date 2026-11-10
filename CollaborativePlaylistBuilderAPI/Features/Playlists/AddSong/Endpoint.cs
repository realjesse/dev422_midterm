using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            var playlist = _addSong.Execute(request.Id, request.SongId);
            return Ok(new Response(playlist.Id, playlist.Name, playlist.Songs.ToList(),
                playlist.CreatedBy, playlist.IsCollaborative));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
