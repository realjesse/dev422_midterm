using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

[ApiController]
[Authorize]
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
            return Ok(_getRankings.Execute(request.Id));
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}
