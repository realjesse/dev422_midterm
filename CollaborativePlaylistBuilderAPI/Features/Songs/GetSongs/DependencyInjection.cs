namespace CollaborativePlaylistBuilderAPI.Features.Songs.GetSongs;

public static class DependencyInjection
{
    public static IServiceCollection AddGetSongs(this IServiceCollection services)
    {
        services.AddScoped<GetSongs>();
        return services;
    }
}
