namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

public record Response(int Id, string Name, List<int> Songs, int CreatedBy, bool IsCollaborative);
