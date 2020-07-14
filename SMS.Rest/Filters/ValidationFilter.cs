using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SMS.Core.Dtos;

// Can be used as controller annotation [ValidateModel]
// Or as a global MVC filter in Startup.cs
namespace SMS.Rest.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // use our custom ErrorResponse object 
                context.Result = new BadRequestObjectResult(
                    new ErrorResponse {                
                        Message = "Validation Errors",
                        Errors = context.ModelState.SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray(),                
                    } 
                );
            }
        }
    }
}