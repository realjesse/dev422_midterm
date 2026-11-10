namespace CollaborativePlaylistBuilderAPI.Features.Playlists.GetRankings;

public static class DependencyInjection
{
    public static IServiceCollection AddGetRankings(this IServiceCollection services)
    {
        services.AddScoped<GetRankings>();
        return services;
    }
}
