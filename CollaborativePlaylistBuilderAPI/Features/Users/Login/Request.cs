using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Login;

public class Request
{
    [Required]
    [StringLength(50)]
    public string? Username { get; set; }

    [Required]
    [StringLength(128)]
    public string? Password { get; set; }
}
