using Lafda.Services;
using Lafda.Services.Interfaces;

namespace Lafda.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBotService, BotService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
