using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Controllers;

[ApiController]
[Route("api/playlists")]
public class PlaylistController : ControllerBase
{
    private static readonly List<Song> Songs = new();
    private readonly IPlaylistRepository _playlistRepository;

    public PlaylistController(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    [HttpPost]
    public ActionResult<Playlist> CreatePlaylist([FromBody] CreatePlaylistRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Playlist name is required.");
        }

        if (request.CreatedBy is not int)
        {
            return BadRequest("Playlist created by is required.");
        }

        if (request.IsCollaborative is not bool)
        {
            return BadRequest("Collaborative is required.");
        }

        var playlist = new Playlist
        {
            Id = 0,
            Name = request.Name,
            Songs = new List<Song>(),
            CreatedBy = request.CreatedBy!.Value,
            IsCollaborative = request.IsCollaborative!.Value
        };
        _playlistRepository.Add(playlist);

        return StatusCode(StatusCodes.Status201Created, playlist);
    }
}
