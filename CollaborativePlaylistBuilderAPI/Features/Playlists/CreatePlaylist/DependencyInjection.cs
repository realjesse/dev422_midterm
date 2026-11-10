namespace CollaborativePlaylistBuilderAPI.Features.Playlists.CreatePlaylist;

public static class DependencyInjection
{
    public static IServiceCollection AddCreatePlaylist(this IServiceCollection services)
    {
        services.AddScoped<CreatePlaylist>();
        return services;
    }
}
