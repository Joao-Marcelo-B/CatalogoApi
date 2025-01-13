using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalogo.Api.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }


    public void OnActionExecuting(ActionExecutingContext context)
    {
        //executa antes do action
        _logger.LogInformation("### Executando -> OnActionExecuting");
        _logger.LogInformation("########################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"ModelState: {context.ModelState.IsValid}");
        _logger.LogInformation("########################################");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //executa depois do action
        _logger.LogInformation("### Executando -> OnActionExecuted");
        _logger.LogInformation("########################################");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"StatusCode: {context.HttpContext.Response.StatusCode}");
        _logger.LogInformation("########################################");
    }
}
