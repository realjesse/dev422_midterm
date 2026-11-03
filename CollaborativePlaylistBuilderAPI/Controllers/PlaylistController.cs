using CollaborativePlaylistBuilderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Controllers;

[ApiController]
[Route("api/playlists")]
public class PlaylistController : ControllerBase
{
    private static readonly List<Playlist> Playlists = new();
    private static readonly List<Song> Songs = new();

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
            Id = Playlists.Count + 1,
            Name = request.Name,
            Songs = new List<Song>(),
            CreatedBy = request.CreatedBy!.Value,
            IsCollaborative = request.IsCollaborative!.Value
        };
        Playlists.Add(playlist);

        return StatusCode(StatusCodes.Status201Created, playlist);
    }
}
