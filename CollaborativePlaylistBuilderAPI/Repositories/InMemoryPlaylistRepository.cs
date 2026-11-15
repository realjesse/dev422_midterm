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

    public Playlist? GetById(int id)
    {
        return Playlists.FirstOrDefault(playlist => playlist.Id == id);
    }

    public Playlist Update(Playlist playlist)
    {
        var index = Playlists.FindIndex(existing => existing.Id == playlist.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException("Playlist not found.");
        }

        Playlists[index] = playlist;
        return playlist;
    }
}
