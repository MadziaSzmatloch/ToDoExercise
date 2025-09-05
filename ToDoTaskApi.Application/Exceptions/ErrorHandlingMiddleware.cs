using Microsoft.AspNetCore.Http;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;

namespace ToDoTaskApi.Application.Exceptions
{

    // Middleware for handling exceptions globally in the application
    public class ErrorHandlingMiddleware : IMiddleware
    {
        // Main middleware method
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Try to execute the next element in the request pipeline
                await next(context);
            }
            catch (Exception ex)
            {
                // If an exception occurs handle it and return an appropriate response
                await HandleExceptionAsync(context, ex);
            }
        }

        // Handles exceptions and prepares the HTTP response
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            // Match the exception type and set the proper status code and message
            switch (exception)
            {
                case ValidationException validationException:
                    // Return 400 Bad Request
                    status = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(new { Errors = validationException.Message });
                    break;

                case NotFoundException notFoundException:
                    // Return 404 Not Found
                    status = HttpStatusCode.NotFound;
                    message = JsonSerializer.Serialize(new { Errors = notFoundException.Message });
                    break;

                default:
                    // Return 500 Internal Server Error
                    status = HttpStatusCode.InternalServerError;
                    message = JsonSerializer.Serialize(new { Error = "An unexpected error occurred." });
                    break;
            }

            // Configure the response with JSON content and proper status code
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(message);
        }
    }
}

