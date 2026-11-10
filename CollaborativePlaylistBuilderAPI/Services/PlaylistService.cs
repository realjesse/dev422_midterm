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

    public Playlist GetRankings(int playlistId)
    {
        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        var rankedSongIds = playlist.Songs
            .Select(songId => _songRepository.GetById(songId)
                ?? throw new KeyNotFoundException("Song not found."))
            .OrderByDescending(song => song.Votes)
            .Select(song => song.Id)
            .ToList();

        return new Playlist
        {
            Id = playlist.Id,
            Name = playlist.Name,
            Songs = rankedSongIds,
            CreatedBy = playlist.CreatedBy,
            IsCollaborative = playlist.IsCollaborative
        };
    }

}
