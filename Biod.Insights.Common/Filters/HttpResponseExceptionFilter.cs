using Biod.Insights.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Biod.Insights.Common.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(new ErrorResponseModel
                {
                    Errors = new[] {exception.Message}
                })
                {
                    StatusCode = (int) exception.Status
                };
                context.ExceptionHandled = true;
            }
        }
    }
}