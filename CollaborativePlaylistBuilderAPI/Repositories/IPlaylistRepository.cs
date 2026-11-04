using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public interface IPlaylistRepository
{
    Playlist Add(Playlist playlist);

    IReadOnlyList<Playlist> GetAll();

    Playlist? GetById(int id);

    Playlist Update(Playlist playlist);
}
