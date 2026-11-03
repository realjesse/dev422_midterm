using CollaborativePlaylistBuilderAPI.Services;
using Xunit;

namespace CollaborativePlaylistBuilderAPI.Tests;

public class PlaylistServiceTests
{
    [Theory]
    [InlineData(42, true)]
    [InlineData(0, false)]
    public void CreatePlaylist_SavesFieldsAndReturnsAssignedId(int createdBy, bool isCollaborative)
    {
        var repository = new RecordingPlaylistRepository();
        var service = new PlaylistService(repository);

        var playlist = service.CreatePlaylist(PlaylistTestData.Request(
            createdBy: createdBy, isCollaborative: isCollaborative));

        Assert.Same(Assert.Single(repository.Added), playlist);
        Assert.Equal(123, playlist.Id);
        Assert.Equal("Road trip", playlist.Name);
        Assert.Equal(createdBy, playlist.CreatedBy);
        Assert.Equal(isCollaborative, playlist.IsCollaborative);
        Assert.Empty(playlist.Songs);
    }

    [Theory]
    [MemberData(nameof(PlaylistTestData.InvalidRequests), MemberType = typeof(PlaylistTestData))]
    public void CreatePlaylist_InvalidRequestThrowsWithoutSaving(
        string? name, int? createdBy, bool? isCollaborative, string expectedMessage)
    {
        var repository = new RecordingPlaylistRepository();
        var service = new PlaylistService(repository);

        var exception = Assert.Throws<ArgumentException>(() => service.CreatePlaylist(
            PlaylistTestData.Request(name, createdBy, isCollaborative)));

        Assert.Equal(expectedMessage, exception.Message);
        Assert.Empty(repository.Added);
    }

    [Fact]
    public void CreatePlaylist_GivesEachPlaylistItsOwnSongIdList()
    {
        var service = new PlaylistService(new RecordingPlaylistRepository());
        var first = service.CreatePlaylist(PlaylistTestData.Request());
        var second = service.CreatePlaylist(PlaylistTestData.Request());

        first.Songs.Add(7);

        Assert.Equal(new[] { 7 }, first.Songs);
        Assert.Empty(second.Songs);
    }
}
