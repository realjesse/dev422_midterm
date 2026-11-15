using System.ComponentModel.DataAnnotations;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;

public class Request
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? SongId { get; set; }
}
