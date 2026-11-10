namespace CollaborativePlaylistBuilderAPI.Models;

public class Playlist
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required List<int> Songs { get; set; }

    public required Guid CreatedBy { get; set; }

    public required bool IsCollaborative { get; set; }
}
