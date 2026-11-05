using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Controllers;

[ApiController]
[Route("api/playlists")]
public class PlaylistController : ControllerBase
{
    private readonly PlaylistService _playlistService;

    public PlaylistController(PlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Playlist>> GetPlaylists()
    {
        return Ok(_playlistService.GetPlaylists());
    }

    [HttpPost]
    public ActionResult<Playlist> CreatePlaylist([FromBody] CreatePlaylistRequest request)
    {
        try
        {
            var playlist = _playlistService.CreatePlaylist(request);
            return StatusCode(StatusCodes.Status201Created, playlist);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}/songs/{songId:int}")]
    public ActionResult<Playlist> AddSong(int id, int songId)
    {
        try
        {
            return Ok(_playlistService.AddSong(id, songId));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost("{id:int}/vote")]
    public ActionResult<Song> VoteSong(int id, [FromBody] VoteSongRequest request)
    {
        if (request.SongId is not int songId || songId <= 0)
        {
            return BadRequest("A positive song ID is required.");
        }

        try
        {
            return Ok(_playlistService.VoteSong(id, songId));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
