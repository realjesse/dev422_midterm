using CollaborativePlaylistBuilderAPI.Models;

namespace CollaborativePlaylistBuilderAPI.Repositories;

public class InMemorySongRepository : ISongRepository
{
    private static readonly List<Song> Songs = new();

    public Song Add(Song song)
    {
        song.Id = Songs.Count + 1;
        Songs.Add(song);
        return song;
    }

    public IReadOnlyList<Song> GetAll()
    {
        return Songs.ToList();
    }

    public Song? GetById(int id)
    {
        return Songs.FirstOrDefault(song => song.Id == id);
    }

    public Song Update(Song song)
    {
        var index = Songs.FindIndex(existing => existing.Id == song.Id);
        if (index < 0)
        {
            throw new KeyNotFoundException("Song not found.");
        }

        Songs[index] = song;
        return song;
    }
}
