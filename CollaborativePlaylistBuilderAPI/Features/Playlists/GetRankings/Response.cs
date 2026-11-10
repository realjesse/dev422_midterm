namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

public record Response(int Id, string Name, List<int> Songs, Guid CreatedBy, bool IsCollaborative);
