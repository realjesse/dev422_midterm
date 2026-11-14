using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public interface ISongRepository
{
    Song Add(Song song);

    IReadOnlyList<Song> GetAll();

    Song? GetById(int id);

    Song Update(Song song);
}
