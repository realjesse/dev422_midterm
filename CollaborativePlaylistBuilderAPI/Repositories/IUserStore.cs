using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public interface IUserStore
{
    bool TryAdd(User user);

    User? GetByUsername(string username);

    User? GetById(Guid id);
}
