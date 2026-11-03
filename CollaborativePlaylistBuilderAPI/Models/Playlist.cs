namespace CollaborativePlaylistBuilderAPI.Models;

public class Playlist
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required List<int> Songs { get; set; }

    public required int CreatedBy { get; set; }

    public required bool IsCollaborative { get; set; }
}
