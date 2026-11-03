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
    public ActionResult<Playlist> CreatePlaylist([FromBody] Playlist playlist)
    {
        if (string.IsNullOrWhiteSpace(playlist.Name))
        {
            return BadRequest("Playlist name is required.");
        }

        playlist.Id = Playlists.Count + 1;
        playlist.Songs = new List<Song>();
        Playlists.Add(playlist);

        return StatusCode(StatusCodes.Status201Created, playlist);
    }
}
