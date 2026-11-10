using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;

public class Request
{
    [Required]
    [StringLength(50)]
    public string? Username { get; set; }
}
