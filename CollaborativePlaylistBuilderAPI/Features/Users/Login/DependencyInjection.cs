namespace CollaborativePlaylistBuilderAPI.Features.Users.Login;

public static class DependencyInjection
{
    public static IServiceCollection AddLogin(this IServiceCollection services)
    {
        services.AddScoped<Login>();
        return services;
    }
}
