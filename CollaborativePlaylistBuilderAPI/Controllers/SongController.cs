using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Controllers;

[ApiController]
[Route("api/songs")]
public class SongController : ControllerBase
{
    private readonly SongService _songService;

    public SongController(SongService songService)
    {
        _songService = songService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Song>> GetSongs()
    {
        return Ok(_songService.GetSongs());
    }

    [HttpPost]
    public ActionResult<Song> CreateSong([FromBody] CreateSongRequest request)
    {
        try
        {
            var song = _songService.CreateSong(request);
            return StatusCode(StatusCodes.Status201Created, song);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
