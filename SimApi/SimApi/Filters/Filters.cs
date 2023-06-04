using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace SimApi.Service.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Log.Information("LogResourceFilter.OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Log.Information("LogResourceFilter.OnResourceExecuting");
        }
    }
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information("LogActionFilter.OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information("LogActionFilter.OnActionExecuting");
        }
    }
    public class LogAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Log.Information("LogAuthorizationFilter.OnAuthorization");
        }
    }
    public class LogResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Log.Information("LogResultFilter.OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Log.Information("LogResultFilter.OnResultExecuting");
        }
    }
    public class LogExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Log.Information("LogExceptionFilter.OnException");
        }
    }
}
