using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;

namespace CollaborativePlaylistBuilderAPI.Tests;

public static class PlaylistTestData
{
    public static IEnumerable<object?[]> InvalidRequests =>
    [
        [null, 1, true, "Playlist name is required."],
        ["", 1, true, "Playlist name is required."],
        [" \t\n", 1, true, "Playlist name is required."],
        ["Road trip", null, true, "Playlist created by is required."],
        ["Road trip", 1, null, "Collaborative is required."]
    ];

    public static CreatePlaylistRequest Request(
        string? name = "Road trip", int? createdBy = 42, bool? isCollaborative = true) => new()
    {
        Name = name!,
        CreatedBy = createdBy,
        IsCollaborative = isCollaborative
    };
}

internal sealed class RecordingPlaylistRepository : IPlaylistRepository
{
    public List<Playlist> Added { get; } = new();

    public Playlist Add(Playlist playlist)
    {
        Added.Add(playlist);
        playlist.Id = 123;
        return playlist;
    }
}
