using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Services;

public class PlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ISongRepository _songRepository;

    public PlaylistService(IPlaylistRepository playlistRepository, ISongRepository songRepository)
    {
        _playlistRepository = playlistRepository;
        _songRepository = songRepository;
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

    public Playlist AddSong(int playlistId, int songId)
    {
        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        if (_songRepository.GetById(songId) is null)
        {
            throw new KeyNotFoundException("Song not found.");
        }

        if (playlist.Songs.Contains(songId))
        {
            return playlist;
        }

        playlist.Songs.Add(songId);
        return _playlistRepository.Update(playlist);
    }
}
