using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

public class Request
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }

    [FromRoute(Name = "songId")]
    public int SongId { get; set; }
}
