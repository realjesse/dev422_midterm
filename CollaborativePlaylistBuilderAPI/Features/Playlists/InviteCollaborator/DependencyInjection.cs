namespace CollaborativePlaylistBuilderAPI.Features.Playlists.InviteCollaborator;

public static class DependencyInjection
{
    public static IServiceCollection AddInviteCollaborator(this IServiceCollection services)
    {
        services.AddScoped<InviteCollaborator>();
        return services;
    }
}
