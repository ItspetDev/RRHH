using FluentValidation;
using RRHH_Backend.Common.Core.Wrapper;
using System.Net;
using System.Text.Json;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var errors = new Dictionary<string, string[]>();
        string message = (exception is ValidationException) ? "Errores de validación" : exception.Message;

        if (exception is ValidationException validationException)
        {
            code = HttpStatusCode.BadRequest;
            errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        var response = new Response<object>
        {
            Data = null,
            IsSuccess = false,
            StatusCode = code,
            Message = message,
            Errores = errors
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
