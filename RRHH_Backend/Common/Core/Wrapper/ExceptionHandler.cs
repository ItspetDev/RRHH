using RRHH_Backend.Common.Core.Wrapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;

public static class ExceptionHandler
{
    private static ILogger _logger;

    public static void Configure(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public static async Task<Response<T>> HandleExceptionAsync<T>(Func<Task<T>> action, string successMessage = null)
    {
        try
        {
            var result = await action();
            return Response<T>.Success(result, successMessage ?? "Operación completada con éxito.");
        }
        catch (Exception ex)
        {
            // Si el logger no está configurado, crear uno temporal para este caso
            if (_logger == null)
            {
                using var loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                });
                _logger = loggerFactory.CreateLogger("FallbackLogger");
            }

            LogDetailedError(_logger, ex);
            string errorMessage = GetSpecificErrorMessage(ex);
            return Response<T>.Failure(errorMessage);
        }
    }

    public static string GetSpecificErrorMessage(Exception ex)
    {
        return ex switch
        {
            ArgumentNullException argEx => $"Valor requerido no proporcionado: {argEx.ParamName}",
            InvalidOperationException customEx => customEx.Message,
            DbUpdateException dbEx => GetDbUpdateErrorMessage(dbEx),
            MySqlException sqlEx => GetMySqlErrorMessage(sqlEx),
            ValidationException valEx => $"Error de validación: {valEx.Message}",
            UnauthorizedAccessException _ => "No tiene permiso para realizar esta acción.",
            TimeoutException _ => "La operación ha tardado demasiado tiempo. Por favor, inténtelo de nuevo más tarde.",
            _ => "Ha ocurrido un error inesperado. Por favor, inténtelo de nuevo más tarde."
        };
    }

    private static string GetDbUpdateErrorMessage(DbUpdateException ex)
    {
        // Buscar en toda la cadena de excepciones internas
        Exception currentException = ex;
        while (currentException != null)
        {
            if (currentException is MySqlException sqlEx)
            {
                return GetMySqlErrorMessage(sqlEx);
            }
            currentException = currentException.InnerException;
        }

        string exMessage = ex.InnerException?.Message ?? ex.Message;
        if (exMessage.Contains("Duplicate entry", StringComparison.OrdinalIgnoreCase))
        {
            return "Ya existe un registro con esa información. Por favor, utilice datos únicos.";
        }
        return "Ocurrió un error al actualizar la base de datos. Por favor, revise los datos e inténtelo de nuevo.";
    }

    private static string GetMySqlErrorMessage(MySqlException ex)
    {
        return ex.Number switch
        {
            1062 => "Ya existe un registro con esa información. Por favor, utilice datos únicos.",
            1451 => "No se puede realizar la operación porque este registro está siendo utilizado en otra parte del sistema.",
            1452 => "La información proporcionada no coincide con los registros existentes. Por favor, verifique los datos.",
            _ => $"Ocurrió un error en la base de datos (Código: {ex.Number}). Por favor, revise los datos e inténtelo de nuevo."
        };
    }

    public static void LogDetailedError(ILogger logger, Exception ex)
    {
        if (logger == null)
            return;

        string detailedMessage = ex switch
        {
            DbUpdateException dbEx => $"Database update error: {dbEx.InnerException?.Message ?? dbEx.Message}",
            MySqlException sqlEx => $"MySQL error {sqlEx.Number}: {sqlEx.Message}",
            ValidationException valEx => $"Validation error: {valEx.Message}",
            _ => $"Unexpected error: {ex.GetType().Name} - {ex.Message}"
        };
        logger.LogError(ex, detailedMessage);
    }
}