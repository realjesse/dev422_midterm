using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetPlaylists;

[ApiController]
[Authorize]
[Route("api/playlists")]
public class Endpoint : ControllerBase
{
    private readonly GetPlaylists _getPlaylists;

    public Endpoint(GetPlaylists getPlaylists)
    {
        _getPlaylists = getPlaylists;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Response>> Handle()
    {
        var playlists = _getPlaylists.Execute()
            .Select(playlist => new Response(playlist.Id, playlist.Name, playlist.Songs.ToList(),
                playlist.CreatedBy, playlist.IsCollaborative, playlist.Collaborators.ToList()))
            .ToList();

        return Ok(playlists);
    }
}
