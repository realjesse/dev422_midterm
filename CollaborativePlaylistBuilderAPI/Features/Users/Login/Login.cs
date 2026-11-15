using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CollaborativePlaylistBuilderAPI.Features.Users.Login;

public class Login
{
    private readonly IUserStore _userStore;
    private readonly IPasswordHasher<User> _passwordHasher;

    public Login(IUserStore userStore, IPasswordHasher<User> passwordHasher)
    {
        _userStore = userStore;
        _passwordHasher = passwordHasher;
    }

    public User? Execute(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length > 50 ||
            string.IsNullOrWhiteSpace(password) || password.Length > 128)
        {
            throw new ArgumentException("Username and password are required and must respect the length limits.");
        }

        var user = _userStore.GetByUsername(username);
        if (user is null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Failed ? null : user;
    }
}
