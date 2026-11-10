using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

public class AddSong
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ISongRepository _songRepository;

    public AddSong(IPlaylistRepository playlistRepository, ISongRepository songRepository)
    {
        _playlistRepository = playlistRepository;
        _songRepository = songRepository;
    }

    public Playlist Execute(int playlistId, int songId)
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
