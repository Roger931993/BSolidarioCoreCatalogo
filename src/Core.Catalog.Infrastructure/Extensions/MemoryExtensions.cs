using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Infrastructure.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Catalog.Infrastructure.Extensions
{
    public static class MemoryExtensions
    {
        public static IServiceCollection AddMemoryCache(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar MemoryCache
            services.AddMemoryCache();
            // Registrar tus servicios
            services.AddSingleton<IMemoryCacheLocalService, MemoryCacheLocalService>();

            return services;
        }
    }
}
