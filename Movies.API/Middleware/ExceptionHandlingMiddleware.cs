using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;


namespace Movies.API.Middleware;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Core.Logging.ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, Core.Logging.ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Proceed to the next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled Exception: {ex.Message} | StackTrace: {ex.StackTrace}");
            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            statusCode = context.Response.StatusCode,
            message = "An unexpected error occurred. Please try again later."
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}
