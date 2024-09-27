using GMS.API.Factories;
using Microsoft.AspNetCore.Diagnostics;

namespace GMS.API.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IHostEnvironment _hostEnv;

        public GlobalExceptionHandler(IHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = ResponseFactory.InternalServerError();

            if(_hostEnv.IsDevelopment()) 
                response = ResponseFactory.InternalServerError(exception.Message);

            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
