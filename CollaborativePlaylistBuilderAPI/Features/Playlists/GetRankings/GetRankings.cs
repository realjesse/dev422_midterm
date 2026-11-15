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

    public Response Execute(int playlistId)
    {
        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        var songs = playlist.Songs
            .Select(songId => _songRepository.GetById(songId)
                ?? throw new KeyNotFoundException("Song not found."))
            .ToList();

        Dictionary<int, int> votes;
        lock (playlist.SongVoters)
        {
            votes = songs.ToDictionary(song => song.Id,
                song => playlist.SongVoters.TryGetValue(song.Id, out var voters) ? voters.Count : 0);
        }

        var rankedSongIds = songs.OrderByDescending(song => votes[song.Id])
            .Select(song => song.Id)
            .ToList();

        return new Response(playlist.Id, playlist.Name, rankedSongIds, playlist.CreatedBy,
            playlist.IsCollaborative, playlist.Collaborators.ToList(), votes);
    }
}
