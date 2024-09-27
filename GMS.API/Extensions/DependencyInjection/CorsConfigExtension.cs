namespace GMS.API.Extensions.DependencyInjection
{
    public static class CorsConfigExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration config)
        {

            var settings = config
                .GetSection(CorsSettings.SectionName)
                .Get<CorsSettings>();

            services.AddCors(builder =>
            {
                builder.AddDefaultPolicy(opt =>
                {
                    opt.WithOrigins(settings!.Origins);
                    opt.AllowAnyHeader();
                    opt.AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
