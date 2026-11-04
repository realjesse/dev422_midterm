using CollaborativePlaylistBuilderAPI.Controllers;
using CollaborativePlaylistBuilderAPI.Models;
using CollaborativePlaylistBuilderAPI.Repositories;
using CollaborativePlaylistBuilderAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CollaborativePlaylistBuilderAPI.Tests;

public class PlaylistControllerTests
{
    [Fact]
    public void CreatePlaylist_ValidRequestReturns201WithSavedPlaylist()
    {
        var repository = new RecordingPlaylistRepository();
        var controller = new PlaylistController(new PlaylistService(repository, new InMemorySongRepository()));

        var response = controller.CreatePlaylist(PlaylistTestData.Request());

        var result = Assert.IsType<ObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        var playlist = Assert.IsType<Playlist>(result.Value);
        Assert.Same(Assert.Single(repository.Added), playlist);
        Assert.Equal(123, playlist.Id);
        Assert.Equal("Road trip", playlist.Name);
        Assert.Equal(42, playlist.CreatedBy);
        Assert.True(playlist.IsCollaborative);
        Assert.Empty(playlist.Songs);
    }

    [Theory]
    [MemberData(nameof(PlaylistTestData.InvalidRequests), MemberType = typeof(PlaylistTestData))]
    public void CreatePlaylist_InvalidRequestReturns400WithValidationMessage(
        string? name, int? createdBy, bool? isCollaborative, string expectedMessage)
    {
        var repository = new RecordingPlaylistRepository();
        var controller = new PlaylistController(new PlaylistService(repository, new InMemorySongRepository()));

        var response = controller.CreatePlaylist(
            PlaylistTestData.Request(name, createdBy, isCollaborative));

        var result = Assert.IsType<BadRequestObjectResult>(response.Result);
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        Assert.Equal(expectedMessage, result.Value);
        Assert.Empty(repository.Added);
    }
}
