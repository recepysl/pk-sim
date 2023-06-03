using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using SimApi.Base;
using System.Net;

namespace SimApi.Service.RestExtension;

public static class DIExtension
{
    public static void AddExceptionHandler(this IApplicationBuilder app)
    {
        //app.UseExceptionHandler(appError =>
        //{
        //    appError.Run(async context =>
        //    {
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        context.Response.ContentType = "application/json";
        //        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //        if (contextFeature != null)
        //        {
        //            Log.Fatal(
        //                $"MethodName={contextFeature.Endpoint} || " +
        //                $"Path={contextFeature.Path} || " +
        //                $"Exception={contextFeature.Error}"
        //                );
        //            await context.Response.WriteAsync(new ApiResponse("Internal Server Error.").ToString());
        //        }
        //    });
        //});
    }

    public static void AddDIExtension(this IApplicationBuilder app)
    {

        //app.Use((context, next) =>
        //{

        //    if (!string.IsNullOrEmpty(context.Request.Path) && context.Request.Path.Value.Contains("favicon"))
        //    {
        //        return next();
        //    }
        //    var singletenService = context.RequestServices.GetRequiredService<SingletonService>();
        //    var scopedService = context.RequestServices.GetRequiredService<ScopedService>();
        //    var transientService = context.RequestServices.GetRequiredService<TransientService>();

        //    singletenService.Counter++;
        //    scopedService.Counter++;
        //    transientService.Counter++;

        //    return next();
        //});

        //app.Run(async context =>
        //{
        //    var singletenService = context.RequestServices.GetRequiredService<SingletonService>();
        //    var scopedService = context.RequestServices.GetRequiredService<ScopedService>();
        //    var transientService = context.RequestServices.GetRequiredService<TransientService>();

        //    if (!string.IsNullOrEmpty(context.Request.Path) && !context.Request.Path.Value.Contains("favicon"))
        //    {
        //        singletenService.Counter++;
        //        scopedService.Counter++;
        //        transientService.Counter++;
        //    }

        //    await context.Response.WriteAsync($"SingletonService: {singletenService.Counter}\n");
        //    await context.Response.WriteAsync($"TransientService: {transientService.Counter}\n");
        //    await context.Response.WriteAsync($"ScopedService: {scopedService.Counter}\n");
        //});
    }

}
