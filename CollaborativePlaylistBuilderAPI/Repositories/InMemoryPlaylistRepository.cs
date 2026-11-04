using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public class InMemoryPlaylistRepository : IPlaylistRepository
{
    private static readonly List<Playlist> Playlists = new();

    public Playlist Add(Playlist playlist)
    {
        playlist.Id = Playlists.Count + 1;
        Playlists.Add(playlist);
        return playlist;
    }

    public IReadOnlyList<Playlist> GetAll()
    {
        return Playlists.ToList();
    }
}
