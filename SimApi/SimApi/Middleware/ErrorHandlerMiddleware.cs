using Serilog;
using System.Net;
using System.Text.Json;

namespace SimApi.Service.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Log.Information("LogErrorHandlerMiddleware.Invoke");
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {

            Log.Fatal(                        
                         $"Path={context.Request.Path} || " +                      
                         $"Method={context.Request.Method} || " +
                         $"Exception={ex.Message}"
                         );

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal server error"));
        }
    }
}
