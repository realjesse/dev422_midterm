using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Services;

public class SongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public IReadOnlyList<Song> GetSongs()
    {
        return _songRepository.GetAll();
    }

    public Song CreateSong(CreateSongRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Song title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Artist))
        {
            throw new ArgumentException("Song artist is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Genre))
        {
            throw new ArgumentException("Song genre is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Duration))
        {
            throw new ArgumentException("Song duration is required.");
        }

        var song = new Song
        {
            Title = request.Title,
            Artist = request.Artist,
            Genre = request.Genre,
            Duration = request.Duration,
            Votes = 0
        };

        return _songRepository.Add(song);
    }
}
