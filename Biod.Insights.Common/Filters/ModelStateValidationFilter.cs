using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Biod.Insights.Common.Filters
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                context.Result = new ObjectResult(new ErrorResponseModel
                {
                    Errors = new[] {string.Join(@"\n", errors)}
                })
                {
                    StatusCode = (int) HttpStatusCode.BadRequest
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}