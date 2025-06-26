using System.Net;
using System.Text.Json.Serialization;

namespace RRHH_Backend.Common.Core.Wrapper;
/// <summary>
/// Esquema predeterminado de respuesta a una solicitud de la API que no devuelve valores.
/// </summary>
public class Response
{

    #region Ctor

    public Response()
        : this(HttpStatusCode.OK)
    { }

    public Response(HttpStatusCode statusCode, string? message)
    {
        IsSuccess = ((int)statusCode).Between(200, 299);
        StatusCode = statusCode;
        Message = message;
    }

    public Response(HttpStatusCode statusCode)
        : this(statusCode, $"{statusCode}({(int)statusCode})")
    { }

    public Response(ApiException exception)
        : this(HttpStatusCode.BadRequest, exception.Message)
    { }

    #endregion

    #region Propiedades

    /// <summary>
    /// Determina si la respuesta del mensaje es correcta o no.
    /// </summary>
    /// 
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; } = false;

    /// <summary>
    /// Devuelve el código de estado Http de la respuesta del API
    /// </summary>
    [JsonPropertyName("statusCode")]
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

    /// <summary>
    /// Devuelve el mensaje de resultado de la respuesta de la solicitud.
    /// En el caso de errores se encuentra el mensaje para el cliente http solicitante.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; } = "Error no definido.";

    [JsonPropertyName("Errores")]
    public string[]? Errores { get; set; } = [];

    #endregion

}

/// <summary>
/// Esquema predeterminado de respuesta a una solicitud de la API que devuelve un resultado de datos.
/// </summary>
/// <typeparam name="T">Tipo de datos del valor resultado de la respuesta.</typeparam>
public sealed class Response<T> : Response
{

    #region Ctor

    public Response()
        : this(HttpStatusCode.NoContent)
    { }

    public Response(T? data, bool? isSuccess, string? message = null)
        : base(data == null ? HttpStatusCode.NoContent : HttpStatusCode.OK)
    {
        Data = data;
        IsSuccess = IsSuccess;
        Message = message;

    }

    public Response(HttpStatusCode statusCode, string? message)
        : base(statusCode, message)
    { }

    public Response(HttpStatusCode statusCode)
        : this(statusCode, $"{statusCode}({(int)statusCode})")
    { }

    public Response(ApiException exception)
        : this(HttpStatusCode.BadRequest, exception.Message)
    { }

    #endregion

    public static Response<T> Success(T data, string message)
    {
        return new Response<T>
        {
            Data = data,
            Message = message,
            IsSuccess = true
        };
    }

    public static Response<T> Failure(string errorMessage)
    {
        return new Response<T>
        {
            Data = default,
            Message = $"Error: {errorMessage}",
            IsSuccess = false
        };
    }
    #region Propiedades

    /// <summary>
    /// Valor resultado de la respuesta a la solicitud de la API.
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }
    [JsonPropertyName("errores")]
    public Dictionary<string, string[]> Errores { get; set; } = new();
    #endregion

}