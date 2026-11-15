using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Songs.CreateSong;

public class CreateSong
{
    private readonly ISongRepository _songRepository;

    public CreateSong(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public Song Execute(string? title, string? artist, string? genre, string? duration)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Song title is required.");
        }

        if (string.IsNullOrWhiteSpace(artist))
        {
            throw new ArgumentException("Song artist is required.");
        }

        if (string.IsNullOrWhiteSpace(genre))
        {
            throw new ArgumentException("Song genre is required.");
        }

        if (string.IsNullOrWhiteSpace(duration))
        {
            throw new ArgumentException("Song duration is required.");
        }

        var song = new Song
        {
            Title = title,
            Artist = artist,
            Genre = genre,
            Duration = duration
        };

        return _songRepository.Add(song);
    }
}
