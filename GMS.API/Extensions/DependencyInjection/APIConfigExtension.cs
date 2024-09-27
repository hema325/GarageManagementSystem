namespace GMS.API.Extensions.DependencyInjection
{
    public static class APIConfigExtension
    {
        public static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = ctx =>
                {
                    var errors = ctx.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                    return new BadRequestObjectResult(ResponseFactory.BadRequest(errors));
                };
            });

            return services;
        }
    }
}
