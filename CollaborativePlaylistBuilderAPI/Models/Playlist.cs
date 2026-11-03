namespace CollaborativePlaylistBuilderAPI.Models;

public class Playlist
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public List<Song> Songs { get; set; } = new();

    public int CreatedBy { get; set; }

    public bool IsCollaborative { get; set; }
}
