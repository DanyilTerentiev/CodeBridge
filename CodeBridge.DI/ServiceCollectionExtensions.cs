using CodeBridge.Application.Interfaces;
using CodeBridge.Application.Services;
using CodeBridge.Domain.Interfaces;
using CodeBridge.Infrastructure.Data;
using CodeBridge.Infrastructure.Repositories;
using ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeBridge.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            options.UseSqlServer(connectionString);
        });
        serviceCollection.AddScoped<IDogRepository, DogRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDogService, DogService>();
        serviceCollection.AddException(AppDomain.CurrentDomain.GetAssemblies());

        return serviceCollection;
    }
}