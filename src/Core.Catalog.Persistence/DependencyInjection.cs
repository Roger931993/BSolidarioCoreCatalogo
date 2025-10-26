using Core.Catalog.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Catalog.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions();
            services.AddDapperService(config);
            services.AddEFService(config);           

            return services;
        }
    }
}
