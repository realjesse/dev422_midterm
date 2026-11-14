namespace CollaborativePlaylistBuilderAPI.Features.Playlists.AddSong;

public static class DependencyInjection
{
    public static IServiceCollection AddSongFeature(this IServiceCollection services)
    {
        services.AddScoped<AddSong>();
        return services;
    }
}
