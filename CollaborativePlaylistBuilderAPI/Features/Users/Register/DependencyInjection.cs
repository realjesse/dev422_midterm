namespace CollaborativePlaylistBuilderAPI.Features.Users.Register;

public static class DependencyInjection
{
    public static IServiceCollection AddRegister(this IServiceCollection services)
    {
        services.AddScoped<Register>();
        return services;
    }
}
