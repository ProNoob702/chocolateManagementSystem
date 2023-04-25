using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateManagementSystem.Infrastructure;

public static class ConfigureServices
{
    public static void ConfigureInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChocolateSystemContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MyWorldDbConnection"));
        });

        services.AddScoped<IMyWorldDbContext>(option => {
           return option.GetService<MyWorldDbContext>();
        });

    }
}