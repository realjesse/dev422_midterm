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

        if (request.CreatedBy is null)
        {
            throw new ArgumentException("Playlist created by is required.");
        }

        if (request.IsCollaborative is null)
        {
            throw new ArgumentException("Collaborative is required.");
        }

        var playlist = new Playlist
        {
            Id = 0,
            Name = request.Name,
            Songs = new List<int>(),
            CreatedBy = request.CreatedBy.Value,
            IsCollaborative = request.IsCollaborative.Value
        };

        return _playlistRepository.Add(playlist);
    }

    public IReadOnlyList<Playlist> GetPlaylists()
    {
        return _playlistRepository.GetAll();
    }
}
