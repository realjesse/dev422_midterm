using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Models;

public class CreateSongRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Artist { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;

    [Required]
    public string Duration { get; set; } = string.Empty;
}
