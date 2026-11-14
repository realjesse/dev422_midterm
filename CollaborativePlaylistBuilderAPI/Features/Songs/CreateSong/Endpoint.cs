using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Songs.CreateSong;

[ApiController]
[Authorize]
[Route("api/songs")]
public class Endpoint : ControllerBase
{
    private readonly CreateSong _createSong;

    public Endpoint(CreateSong createSong)
    {
        _createSong = createSong;
    }

    [HttpPost]
    public ActionResult<Response> Handle([FromBody] Request request)
    {
        try
        {
            var song = _createSong.Execute(request.Title, request.Artist, request.Genre, request.Duration);
            var response = new Response(song.Id, song.Title, song.Artist, song.Genre, song.Duration);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
