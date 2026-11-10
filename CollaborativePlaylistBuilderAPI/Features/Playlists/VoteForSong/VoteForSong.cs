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

    public Song Execute(int playlistId, int songId)
    {
        var playlist = _playlistRepository.GetById(playlistId)
            ?? throw new KeyNotFoundException("Playlist not found.");

        if (!playlist.Songs.Contains(songId))
        {
            throw new KeyNotFoundException("Song is not in this playlist.");
        }

        var song = _songRepository.GetById(songId)
            ?? throw new KeyNotFoundException("Song not found.");

        song.Votes++;
        return _songRepository.Update(song);
    }
}
