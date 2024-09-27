using FSharp.Data;
using GMS.Shared.Dtos.Responses.Global;
using System.Net;

namespace GMS.API.Factories
{
    public static class ResponseFactory
    {
        public static Response<object> StatusCode(int status, string? message = null)
           => new Response<object>(status, message);
        
        public static Response<object> BadRequest(string? message = null, string[]? errors = null)
           => new Response<object>(HttpStatusCodes.BadRequest, message, errors);        
        
        public static Response<object> BadRequest(string[] errors)
           => new Response<object>(HttpStatusCodes.BadRequest, errors: errors);

        public static Response<object> Unauthorized(string? message = null)
            => new Response<object>(HttpStatusCodes.Unauthorized, message);

        public static Response<object> Forbidden(string? message = null)
            => new Response<object>(HttpStatusCodes.Forbidden, message);

        public static Response<object> NotFound(string? message = null)
            => new Response<object>(HttpStatusCodes.NotFound, message);

        public static Response<object> Conflict(string? message = null)
            => new Response<object>(HttpStatusCodes.Conflict, message);

        public static Response<object> InternalServerError(string? message = null)
            => new Response<object>(HttpStatusCodes.InternalServerError, message);

        public static Response<object> Ok(string? message = null) 
            => new Response<object>(HttpStatusCodes.OK, message);      
        
        public static Response<TData> Ok<TData>(TData data) 
            => new Response<TData>(HttpStatusCodes.OK, data);
    }
}
