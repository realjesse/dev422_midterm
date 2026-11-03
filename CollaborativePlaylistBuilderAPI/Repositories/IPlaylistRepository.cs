using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public interface IPlaylistRepository
{
    Playlist Add(Playlist playlist);
}
