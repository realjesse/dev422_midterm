using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Register;

public class Request
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Username { get; set; }

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public string? Password { get; set; }
}
