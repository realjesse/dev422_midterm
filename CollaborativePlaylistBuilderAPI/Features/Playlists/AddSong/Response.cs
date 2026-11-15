namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

public record Response(int Id, string Name, List<int> Songs, Guid CreatedBy,
    bool IsCollaborative, List<Guid> Collaborators);
