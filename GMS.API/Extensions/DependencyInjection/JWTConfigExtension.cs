using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GMS.API.Extensions.DependencyInjection
{
    public static class JWTConfigExtension
    {
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(opt=>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    
            })
            .AddJwtBearer(opt =>
            {
                var settings = config
                    .GetSection(JWTSettings.SectionName)
                    .Get<JWTSettings>();

                opt.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SigningKey))
                };
            });

            return services;
        }
    }
}
