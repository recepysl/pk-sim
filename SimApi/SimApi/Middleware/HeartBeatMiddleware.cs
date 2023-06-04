using Serilog;
using System.Text.Json;

namespace SimApi.Service.Middleware;

public class HeartBeatMiddleware
{
    private readonly RequestDelegate next;

    public HeartBeatMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Log.Information("LogHeartBeatMiddleware.Invoke");
        if (context.Request.Path.StartsWithSegments("/heartbeat"))
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize("Hello from server"));
            context.Response.StatusCode = 200;
            return;
        }

        await next.Invoke(context);
    }
}
