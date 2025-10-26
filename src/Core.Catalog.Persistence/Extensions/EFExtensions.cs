using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Persistence.Contexts;
using Core.Catalog.Persistence.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Catalog.Persistence.Extensions
{
    public static class PersistenceServiceExtensions
    {
        public static IServiceCollection AddEFService(this IServiceCollection services, IConfiguration configuration)
        {
            #region SQL
            // Configura la conexión a la base de datos.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Catalogo"));
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Logs"));
            });
            #endregion
       

            // Configura la conexión a la base de datos.
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddTransient<ILoggRepository, LoggRepository>();


            return services;
        }
    }
}
