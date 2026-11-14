using Microsoft.AspNetCore.Mvc;

namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

public class Request
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }
}
