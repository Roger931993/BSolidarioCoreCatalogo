using Core.Catalog.Application.Common;
using Core.Catalog.Application.Interfaces;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Catalog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ApplicationMappingProfile));

            services.AddScoped<IErrorCatalogException, ErrorCatalogException>();

            services.AddMediatR(gfc => gfc.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IPermissionService, PermissionService>();

            return services;
        }
    }
}
