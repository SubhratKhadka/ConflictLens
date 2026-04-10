using Lafda.Extensions;

namespace Lafda;

public static class DiConfigs
{
    public static void ConfigureExtensions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddJwtAuth(configuration);
        services.AddInfrastructures(configuration);
        services.AddApplication();
        services.AddPersistence();
    }
}
