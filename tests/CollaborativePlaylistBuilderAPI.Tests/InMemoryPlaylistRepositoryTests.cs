using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;
using Xunit;

namespace CollaborativePlaylistBuilderAPI.Tests;

public class InMemoryPlaylistRepositoryTests
{
    [Fact]
    public void Add_AssignsIncreasingIdsAndPreservesPlaylistDataAcrossRepositoryInstances()
    {
        var first = CreatePlaylist("First");
        var second = CreatePlaylist("Second");

        var firstResult = new InMemoryPlaylistRepository().Add(first);
        var secondResult = new InMemoryPlaylistRepository().Add(second);

        Assert.Same(first, firstResult);
        Assert.Same(second, secondResult);
        Assert.True(firstResult.Id > 0);
        Assert.Equal(firstResult.Id + 1, secondResult.Id);
        Assert.Equal("First", firstResult.Name);
        Assert.Equal("Second", secondResult.Name);
        Assert.Equal(42, firstResult.CreatedBy);
        Assert.True(firstResult.IsCollaborative);
        Assert.Equal(new[] { 10, 20 }, firstResult.Songs);
    }

    private static Playlist CreatePlaylist(string name) => new()
    {
        Id = 0,
        Name = name,
        CreatedBy = 42,
        IsCollaborative = true,
        Songs = new List<int> { 10, 20 }
    };
}
