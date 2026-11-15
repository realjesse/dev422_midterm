using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Songs.GetSongs;

public class GetSongs
{
    private readonly ISongRepository _songRepository;

    public GetSongs(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public IReadOnlyList<Song> Execute()
    {
        return _songRepository.GetAll();
    }
}
