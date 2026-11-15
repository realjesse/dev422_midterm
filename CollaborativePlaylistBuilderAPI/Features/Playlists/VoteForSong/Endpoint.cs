using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(claim, out var userId) || userId == Guid.Empty)
        {
            return Unauthorized();
        }

        if (request.SongId is not int songId || songId <= 0)
        {
            return BadRequest("A positive song ID is required.");
        }

        try
        {
            var result = _voteForSong.Execute(id, songId, userId);
            var song = result.Song;
            return Ok(new Response(song.Id, song.Title, song.Artist, song.Genre, song.Duration, result.Votes));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
