namespace CollaborativePlaylistBuilderAPI.Features.Playlists.VoteForSong;

public static class DependencyInjection
{
    public static IServiceCollection AddVoteForSong(this IServiceCollection services)
    {
        services.AddScoped<VoteForSong>();
        return services;
    }
}
