using Lafda.Data;
using Microsoft.EntityFrameworkCore;

namespace Lafda.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructures(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
        );

        return services;
    }
}
