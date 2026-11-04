using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public interface ISongRepository
{
    IReadOnlyList<Song> GetAll();
}
