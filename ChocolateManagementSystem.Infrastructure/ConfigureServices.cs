using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChocolateManagementSystem.Infrastructure;

public static class ConfigureServices
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ChocolateSystemContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SQLDatabase"));
        });

        services.AddScoped<IChocolateSystemContext, ChocolateSystemContext>();
    }
}