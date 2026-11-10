namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetPlaylists;

public record Response(int Id, string Name, List<int> Songs, Guid CreatedBy, bool IsCollaborative);
