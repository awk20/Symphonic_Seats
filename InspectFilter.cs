using Microsoft.AspNetCore.Mvc.Filters;

namespace SymphonicSeats2;

public class InspectFilter : IActionFilter
{
    private ILogger<InspectFilter> _logger;

    public InspectFilter(ILogger<InspectFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Will only log when context is on api path
        if (context.HttpContext.Request.Path.Value.Contains("api"))
        {
            _logger.LogInformation("Request for {path}", context.HttpContext.Request.Path);
        }
    }

}