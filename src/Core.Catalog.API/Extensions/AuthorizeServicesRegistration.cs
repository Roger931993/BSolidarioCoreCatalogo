using Core.Catalog.API.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Core.Catalog.API.Extensions
{
    public static class AuthorizeServicesRegistration
    {
        public static IServiceCollection AddAuthorizeServiceJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationSettings>(configuration.GetSection("AuthenticationSettings"));

            var authSettings = configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();

            if (authSettings.UseNoValidation)
            {
                services.AddAuthentication("NoValidation")
                        .AddScheme<AuthenticationSchemeOptions, NoValidationAuthenticationHandler>("NoValidation", null);
            }
            else
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration["Settings:JwtAuthority"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = configuration["Settings:ValidateAudience"] == "true",
                        ValidAudience = configuration["Settings:JwtAudience"],
                        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Settings:JwtKey"]!))
                    };
                });
            }
            return services;
        }
    }
    public class AuthenticationSettings
    {
        public bool UseNoValidation { get; set; }
    }
}
