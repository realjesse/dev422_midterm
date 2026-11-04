using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public class InMemorySongRepository : ISongRepository
{
    private static readonly List<Song> Songs = new();

    public IReadOnlyList<Song> GetAll()
    {
        return Songs.ToList();
    }
}
