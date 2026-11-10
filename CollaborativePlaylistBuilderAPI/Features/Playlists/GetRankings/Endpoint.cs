using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

[ApiController]
[Route("api/playlists/{id:int}/rankings")]
public class Endpoint : ControllerBase
{
    private readonly GetRankings _getRankings;

    public Endpoint(GetRankings getRankings)
    {
        _getRankings = getRankings;
    }

    [HttpGet]
    public ActionResult<Response> Handle([FromRoute] Request request)
    {
        try
        {
            var playlist = _getRankings.Execute(request.Id);
            return Ok(new Response(playlist.Id, playlist.Name, playlist.Songs,
                playlist.CreatedBy, playlist.IsCollaborative));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
