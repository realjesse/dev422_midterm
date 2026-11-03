namespace CollaborativePlaylistBuilderAPI.Models;

public class Song
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Artist { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public string Duration { get; set; } = string.Empty;

    public int Votes { get; set; }
}
