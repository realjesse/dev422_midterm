namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

public record Response(int Id, string Name, List<int> Songs, int CreatedBy, bool IsCollaborative);
