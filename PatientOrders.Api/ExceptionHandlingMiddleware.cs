using System.Net.Mime;
using System.Text.Json;

namespace PatientOrders.Api;

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
        catch (ApiException apiException)
        {
            await WriteErrorResponse(context, apiException.HttpStatusCode, new
            {
                apiException.Code,
                Message = apiException.DisplayMessage,
                apiException.Details
            });
        }
        catch (Exception exception)
        {
            await WriteErrorResponse(context, StatusCodes.Status500InternalServerError, new
            {
                Code = "InternalServerError",
                Message = ""
            });
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, int statusCode, object body)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(body));
    }
}