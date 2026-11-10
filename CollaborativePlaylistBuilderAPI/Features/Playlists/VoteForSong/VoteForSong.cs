using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;

public class VoteForSong
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ISongRepository _songRepository;

    public VoteForSong(IPlaylistRepository playlistRepository, ISongRepository songRepository)
    {
        _playlistRepository = playlistRepository;
        _songRepository = songRepository;
    }

    public Result Execute(int playlistId, int songId, Guid currentUserId)
    {
        if (currentUserId == Guid.Empty)
        {
            throw new ArgumentException("A voter ID is required.");
        }

        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        if (!playlist.Songs.Contains(songId))
        {
            throw new KeyNotFoundException("Song is not in this playlist.");
        }

        var song = _songRepository.GetById(songId)
            ?? throw new KeyNotFoundException("Song not found.");

        lock (playlist.SongVoters)
        {
            if (!playlist.SongVoters.TryGetValue(songId, out var voters))
            {
                voters = new HashSet<Guid>();
                playlist.SongVoters[songId] = voters;
            }

            if (voters.Add(currentUserId))
            {
                _playlistRepository.Update(playlist);
            }

            return new Result(song, voters.Count);
        }
    }
}
