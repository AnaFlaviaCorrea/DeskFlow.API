using System.Net;
using System.Text.Json;

namespace DeskFlow.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await TratarExcecaoAsync(context, exception);
        }
    }

    private static async Task TratarExcecaoAsync(
        HttpContext context,
        Exception exception
    )
    {
        var statusCode = exception switch
        {
            KeyNotFoundException => HttpStatusCode.NotFound,

            ArgumentException => HttpStatusCode.BadRequest,

            InvalidOperationException => HttpStatusCode.Conflict,

            _ => HttpStatusCode.InternalServerError
        };

        var resposta = new
        {
            status = (int)statusCode,
            erro = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(resposta);

        await context.Response.WriteAsync(json);
    }
}