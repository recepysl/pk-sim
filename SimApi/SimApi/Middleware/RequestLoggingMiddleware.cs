using Serilog;
using SimApi.Base;
using System.Net;

namespace SimApi.Service.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate next;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            Log.Debug($"" +
                $"MiddleWareLogger init Method: ${context.Request.Method} || " +
                $"Path: ${context.Request.Path}");

            await next(context);

        }
        catch (Exception ex)
        {
            Log.Fatal(
                       $"MiddleWareLogger Path={context.Request.Path} || " +
                       $"Method={context.Request.Method} || " +
                       $"Exception={ex.Message}"
                       );

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new ApiResponse("Internal Server Error.").ToString());
        }
        finally
        {
            var ResponseText = "";
            var RequestText = "";

            using (var reader = new MemoryStream())
            {
                context.Response.Body = reader;
                var sr = new StreamReader(context.Response.Body);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                ResponseText = await sr.ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }

            using (var reader = new MemoryStream())
            {
                context.Request.Body = reader;
                var sr = new StreamReader(context.Request.Body);
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                RequestText = await sr.ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            Log.Fatal(
                      $"MiddleWareLogger Path={context.Request.Path} || " +
                      $"Method={context.Request.Method} || " +
                      $"Request={RequestText} || " +
                      $"Response={ResponseText}"
                      );
        }
    }


}
