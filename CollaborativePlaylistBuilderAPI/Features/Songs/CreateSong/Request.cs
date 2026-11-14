using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Songs.CreateSong;

public class Request
{
    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Artist { get; set; }

    [Required]
    public string? Genre { get; set; }

    [Required]
    public string? Duration { get; set; }
}
