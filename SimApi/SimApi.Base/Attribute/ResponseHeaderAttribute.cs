using Microsoft.AspNetCore.Mvc.Filters;

namespace SimApi.Base;

public class ResponseHeaderAttribute : ActionFilterAttribute
{
    private readonly string name;
    private readonly string value;

    public ResponseHeaderAttribute(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
  
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        context.HttpContext.Response.Headers.Add(name, value);
        base.OnActionExecuted(context);
    }
}
