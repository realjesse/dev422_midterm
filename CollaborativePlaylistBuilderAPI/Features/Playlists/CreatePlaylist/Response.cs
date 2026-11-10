namespace CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;

public record Response(int Id, string Name, List<int> Songs, Guid CreatedBy, bool IsCollaborative);
