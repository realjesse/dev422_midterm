using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;

public class InviteCollaborator
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IUserStore _userStore;

    public InviteCollaborator(IPlaylistRepository playlistRepository, IUserStore userStore)
    {
        _playlistRepository = playlistRepository;
        _userStore = userStore;
    }

    public Playlist Execute(int playlistId, Guid currentUserId, string? username)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Trim().Length > 50)
        {
            throw new ArgumentException("A username of at most 50 characters is required.");
        }

        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        if (currentUserId == Guid.Empty || playlist.CreatedBy != currentUserId)
        {
            throw new UnauthorizedAccessException("Only the playlist owner can invite collaborators.");
        }

        if (!playlist.IsCollaborative)
        {
            throw new InvalidOperationException("Playlist is not collaborative.");
        }

        var user = _userStore.GetByUsername(username)
            ?? throw new KeyNotFoundException("User not found.");

        if (user.Id == playlist.CreatedBy)
        {
            throw new ArgumentException("The owner already has editing access.");
        }

        if (playlist.Collaborators.Contains(user.Id))
        {
            return playlist;
        }

        playlist.Collaborators.Add(user.Id);
        return _playlistRepository.Update(playlist);
    }
}
