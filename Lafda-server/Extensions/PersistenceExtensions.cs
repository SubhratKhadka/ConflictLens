using Lafda.Repositories;
using Lafda.Repositories.Interfaces;

namespace Lafda.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IBotRepository, BotRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITheoryRepository, TheoryRepository>();
        services.AddScoped<IMainEventRepository, MainEventRepository>();

        return services;
    }
}
