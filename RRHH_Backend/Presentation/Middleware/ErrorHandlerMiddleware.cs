using RRHH_Backend.Common.Core.Wrapper;
using System.Net;
using System.Text.Json;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: " + ex.ToString());
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new Response<object>();

        var errorMessage = ExceptionHandler.GetSpecificErrorMessage(exception);
        ExceptionHandler.LogDetailedError(_logger, exception);

        response.IsSuccess = false;
        response.Message = errorMessage;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
}
