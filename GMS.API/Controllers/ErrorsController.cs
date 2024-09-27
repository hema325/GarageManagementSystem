namespace GMS.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(Router.Errors.GetErrorResponse)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult GetErrorResponse(int status)
        {
            return StatusCode(status, ResponseFactory.StatusCode(status));
        }
    }
}
