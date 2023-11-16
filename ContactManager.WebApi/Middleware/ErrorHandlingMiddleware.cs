using ContactManager.Business.Exceptions;
using System.Net;
using System.Text.Json;

namespace ContactManager.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException notFoundException)
            {
                var code = HttpStatusCode.NotFound;

                string message = string.Empty;

                if (notFoundException.Message == null)
                {
                    message = "An entity was not found.";
                }
                else
                {
                    message = notFoundException.Message;
                }

                await HandleExceptionAsync(context, code, message);
            }
            catch (Exception exception)
            {
                var code = HttpStatusCode.InternalServerError;
                var message = exception.Message;

                await HandleExceptionAsync(context, code, message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, string message)
        {
            var result = JsonSerializer.Serialize(message);

            var httpResponse = context.Response;

            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int)code;

            await httpResponse.WriteAsync(result);
        }
    }
}