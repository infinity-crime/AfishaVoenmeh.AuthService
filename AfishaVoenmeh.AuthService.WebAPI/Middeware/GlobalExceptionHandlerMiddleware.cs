using Microsoft.AspNetCore.Mvc;

namespace AfishaVoenmeh.AuthService.WebAPI.Middeware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next, 
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception: {ex.Source}, {ex.Message}, {ex.InnerException?.Message}");

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path,
            Type = ex.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
            Detail = $"{ex.Message} | {ex.InnerException?.Message}"
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}
