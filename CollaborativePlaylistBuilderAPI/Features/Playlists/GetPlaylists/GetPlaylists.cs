using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetPlaylists;

public class GetPlaylists
{
    private readonly IPlaylistRepository _playlistRepository;

    public GetPlaylists(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    public IReadOnlyList<Playlist> Execute()
    {
        return _playlistRepository.GetAll();
    }
}
