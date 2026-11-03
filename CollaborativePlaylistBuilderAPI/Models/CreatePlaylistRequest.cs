using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Models;

public class CreatePlaylistRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int? CreatedBy { get; set; }

    [Required]
    public bool? IsCollaborative { get; set; }
}
