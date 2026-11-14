using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;

public class Request
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public bool? IsCollaborative { get; set; }
}
