using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Notifications;
using SupplierRegServer.Business.Services;
using SupplierRegServer.Data.Context;
using SupplierRegServer.Data.Repositories;

namespace SupplierRegServer.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        // Data
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        // Bussiness
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<INotifier, Notifier>();

        return services;
    }
}
