using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ToDoTaskApi.Application.Exceptions
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            switch (exception)
            {
                case ValidationException validationException:
                    status = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(new { Errors = validationException.Message });
                    break;

                case NotFoundException notFoundException:
                    status = HttpStatusCode.NotFound;
                    message = JsonSerializer.Serialize(new { Errors = notFoundException.Message });
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    message = JsonSerializer.Serialize(new { Error = "An unexpected error occurred." });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(message);
        }
    }
}

