using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Register;

public class Register
{
    private readonly IUserStore _userStore;
    private readonly IPasswordHasher<User> _passwordHasher;

    public Register(IUserStore userStore, IPasswordHasher<User> passwordHasher)
    {
        _userStore = userStore;
        _passwordHasher = passwordHasher;
    }

    public User Execute(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Trim().Length is < 3 or > 50)
        {
            throw new ArgumentException("Username must be between 3 and 50 characters.");
        }

        if (string.IsNullOrWhiteSpace(password) || password.Length is < 8 or > 128)
        {
            throw new ArgumentException("Password must be between 8 and 128 characters.");
        }

        var user = new User(Guid.NewGuid(), username.Trim(), string.Empty);
        user = user with { PasswordHash = _passwordHasher.HashPassword(user, password) };

        if (!_userStore.TryAdd(user))
        {
            throw new InvalidOperationException("Username is already registered.");
        }

        return user;
    }
}
