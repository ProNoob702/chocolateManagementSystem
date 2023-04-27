using ChocolateManagementSystem.Application.Common.Interfaces;
using ChocolateManagementSystem.Infrastructure.Context;
using ChocolateManagementSystem.Infrastructure.Repositories;
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

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IChocolateBarsRepository, ChocolateBarsRepository>();
        services.AddScoped<IChocolateFactoryRepository, ChocolateFactoryRepository>();
        services.AddScoped<IWholesalersRepository, WholesalersRepository>();
        services.AddScoped<IWholesalerChocolateStocksRepository, WholesalerChocolateStocksRepository>();
    }
}