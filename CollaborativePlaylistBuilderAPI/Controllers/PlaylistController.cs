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
}
