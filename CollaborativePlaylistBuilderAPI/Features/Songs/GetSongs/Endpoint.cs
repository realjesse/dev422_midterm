using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Songs.GetSongs;

[ApiController]
[Authorize]
[Route("api/songs")]
public class Endpoint : ControllerBase
{
    private readonly GetSongs _getSongs;

    public Endpoint(GetSongs getSongs)
    {
        _getSongs = getSongs;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Response>> Handle()
    {
        var songs = _getSongs.Execute()
            .Select(song => new Response(song.Id, song.Title, song.Artist, song.Genre, song.Duration, song.Votes))
            .ToList();

        return Ok(songs);
    }
}
