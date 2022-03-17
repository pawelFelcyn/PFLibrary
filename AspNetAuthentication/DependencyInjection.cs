using AspNetAuthentication.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AspNetAuthentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services, IAuthenticationSettings authenticationSettings)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new()
                    {
                        ValidAudience = authenticationSettings.JwtIssuer,
                        ValidIssuer = authenticationSettings.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                    };
                });

            return services;
        }
    }
}
