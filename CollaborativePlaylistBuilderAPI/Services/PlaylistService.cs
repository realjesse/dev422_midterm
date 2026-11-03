using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Services;

public class PlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;

    public PlaylistService(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    public Playlist CreatePlaylist(CreatePlaylistRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Playlist name is required.");
        }

        if (request.CreatedBy is not int createdBy)
        {
            throw new ArgumentException("Playlist created by is required.");
        }

        if (request.IsCollaborative is not bool isCollaborative)
        {
            throw new ArgumentException("Collaborative is required.");
        }

        var playlist = new Playlist
        {
            Id = 0,
            Name = request.Name,
            Songs = new List<Song>(),
            CreatedBy = createdBy,
            IsCollaborative = isCollaborative
        };

        return _playlistRepository.Add(playlist);
    }
}
