using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public class InMemorySongRepository : ISongRepository
{
    private static readonly List<Song> Songs = new();

    public Song Add(Song song)
    {
        song.Id = Songs.Count + 1;
        Songs.Add(song);
        return song;
    }

    public IReadOnlyList<Song> GetAll()
    {
        return Songs.ToList();
    }
}
