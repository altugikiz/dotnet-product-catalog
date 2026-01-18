using System.Net;
using System.Text.Json;
using ProductCatalog.API.Exceptions;

namespace ProductCatalog.API.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,      // 404
            BusinessException => (int)HttpStatusCode.BadRequest,    // 400
            _ => (int)HttpStatusCode.InternalServerError            // 500
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Detailed = statusCode == 500 ? "Internal Server Error" : "Operation failed."
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);

        return context.Response.WriteAsync(jsonResponse);
    }
}