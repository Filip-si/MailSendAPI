using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Infrastructure.Exceptions
{
  public class BusinessExceptionFilter : IActionFilter
  {
    private readonly ILogger _logger;

    public BusinessExceptionFilter(ILogger logger)
    {
      _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
      if(filterContext.Exception is BusinessException businessException)
      {
        filterContext.Result = new ObjectResult(new { businessException.Message, businessException.Code })
        {
          StatusCode = StatusCodes.Status409Conflict
        };

        filterContext.ExceptionHandled = true;

        _logger.Warning(businessException, businessException.ToString());
        return;
      }

      if(filterContext.Exception is BusinessNotFoundException businessNotFoundException)
      {
        filterContext.Result = new ObjectResult(new { businessNotFoundException.Message })
        {
          StatusCode = StatusCodes.Status404NotFound
        };
        filterContext.ExceptionHandled = true;

        _logger.Warning(businessNotFoundException, businessNotFoundException.ToString());
        return;
      }

      if(filterContext.Exception is { } exception)
      {
        filterContext.Result = new ObjectResult(new { exception.Message })
        {
          StatusCode = StatusCodes.Status500InternalServerError
        };
        filterContext.ExceptionHandled = true;

        _logger.Warning(exception, exception.Message);
      }
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
    }
  }
}
