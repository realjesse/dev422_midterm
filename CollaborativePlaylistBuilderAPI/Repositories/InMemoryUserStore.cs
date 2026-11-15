using System.Collections.Concurrent;
using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public class InMemoryUserStore : IUserStore
{
    private readonly ConcurrentDictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

    public bool TryAdd(User user)
    {
        return _users.TryAdd(user.Username.Trim(), user);
    }

    public User? GetByUsername(string username)
    {
        return _users.GetValueOrDefault(username.Trim());
    }

    public User? GetById(Guid id)
    {
        return _users.Values.FirstOrDefault(user => user.Id == id);
    }
}
