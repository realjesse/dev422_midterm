using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;

[ApiController]
[Authorize]
[Route("api/playlists/{id:int}/vote")]
public class Endpoint : ControllerBase
{
    private readonly VoteForSong _voteForSong;

    public Endpoint(VoteForSong voteForSong)
    {
        _voteForSong = voteForSong;
    }

    [HttpPost]
    public ActionResult<Response> Handle(int id, [FromBody] Request request)
    {
        if (request.SongId is not int songId || songId <= 0)
        {
            return BadRequest("A positive song ID is required.");
        }

        try
        {
            var song = _voteForSong.Execute(id, songId);
            return Ok(new Response(song.Id, song.Title, song.Artist, song.Genre, song.Duration, song.Votes));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
