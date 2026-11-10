namespace CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;

public record Response(int Id, string Name, List<int> Songs, Guid CreatedBy,
    bool IsCollaborative, List<Guid> Collaborators);
