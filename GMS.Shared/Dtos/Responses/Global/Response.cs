using FSharp.Data;
using GMS.Shared.Constants;

namespace GMS.Shared.Dtos.Responses.Global
{
    public class Response<TData> 
    {
        public int Status { get; init; }
        public string? Message { get; init; }
        public string[]? Errors { get; init; }
        public TData? Data { get; init; }
        public bool Succeeded { get; init; }

        public Response(int status, string? message = null, string[]? errors = null)
        {
            Status = status;
            Message = message ?? DefaultMessage(status);
            Errors = errors;
            Succeeded = status >= 200 && status <= 299;
        }
        

        public Response(int status, TData data): this(status)
        {
            Data = data;
        }

        public Response()
        {
            
        }

        private string? DefaultMessage(int status) => status switch 
        {
            HttpStatusCodes.OK => ErrorMessages.Ok,
            HttpStatusCodes.BadRequest => ErrorMessages.BadRequest,
            HttpStatusCodes.Unauthorized => ErrorMessages.Unauthorized,
            HttpStatusCodes.Forbidden => ErrorMessages.Forbidden,
            HttpStatusCodes.NotFound => ErrorMessages.NotFound,
            HttpStatusCodes.Conflict => ErrorMessages.Conflict,
            HttpStatusCodes.InternalServerError => ErrorMessages.InternalServerError,
            _=> null
        };
    }
}
