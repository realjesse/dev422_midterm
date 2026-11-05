using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Models;

public class VoteSongRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? SongId { get; set; }
}
