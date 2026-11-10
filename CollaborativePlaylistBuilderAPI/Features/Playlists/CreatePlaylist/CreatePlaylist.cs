using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;

public class CreatePlaylist
{
    private readonly IPlaylistRepository _playlistRepository;

    public CreatePlaylist(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    public Playlist Execute(string? name, int? createdBy, bool? isCollaborative)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Playlist name is required.");
        }

        if (createdBy is null)
        {
            throw new ArgumentException("Playlist created by is required.");
        }

        if (isCollaborative is null)
        {
            throw new ArgumentException("Collaborative is required.");
        }

        var playlist = new Playlist
        {
            Name = name,
            Songs = new List<int>(),
            CreatedBy = createdBy.Value,
            IsCollaborative = isCollaborative.Value
        };

        return _playlistRepository.Add(playlist);
    }
}
