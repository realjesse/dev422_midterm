namespace CollaborativePlaylistBuilderAPI.Features.Songs.CreateSong;

public static class DependencyInjection
{
    public static IServiceCollection AddCreateSong(this IServiceCollection services)
    {
        services.AddScoped<CreateSong>();
        return services;
    }
}
