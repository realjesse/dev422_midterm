using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

public class GetRankings
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ISongRepository _songRepository;

    public GetRankings(IPlaylistRepository playlistRepository, ISongRepository songRepository)
    {
        _playlistRepository = playlistRepository;
        _songRepository = songRepository;
    }

    public Playlist Execute(int playlistId)
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
            Collaborators = playlist.Collaborators.ToList(),
            IsCollaborative = playlist.IsCollaborative
        };
    }
}
