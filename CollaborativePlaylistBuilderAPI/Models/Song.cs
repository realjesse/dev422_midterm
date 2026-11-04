namespace CollaborativePlaylistBuilderAPI.Models;

public class Song
{
    public int Id { get; set; }
    
    public required string Title { get; set; }

    public required string Artist { get; set; }

    public required string Genre { get; set; }

    public required string Duration { get; set; }

    public int Votes { get; set; }
}
