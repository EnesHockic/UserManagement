using API.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Infrastructure.Configurations
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;
            var stackTrace = string.Empty;
            string message = "";
            var exceptionType = ex.GetType();

            if(exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }else if(exceptionType == typeof(ValidationException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(DuplicateException))
            {
                message = ex.Message;
                statusCode = HttpStatusCode.Conflict;
                stackTrace = ex.StackTrace;
            }
            else
            {
                message = "Internal Server Error";
                statusCode = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
            }

            var exceptionRes = JsonSerializer.Serialize(new { error = message, stackTrace = stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            return context.Response.WriteAsync(exceptionRes);
        }
    }
}
