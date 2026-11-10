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

    [HttpGet("{id:int}/rankings")]
    public ActionResult<Playlist> GetRankings(int id)
    {
        try
        {
            return Ok(_playlistService.GetRankings(id));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
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

}
