namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetPlaylists;

public static class DependencyInjection
{
    public static IServiceCollection AddGetPlaylists(this IServiceCollection services)
    {
        services.AddScoped<GetPlaylists>();
        return services;
    }
}
