using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SMS.Rest.Models;

// Can be used as controller annotation [ValidateModel]
// Or as a global MVC filter in Startup.cs

namespace SMS.Rest.Validators
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // use our custom response object 
                context.Result = new BadRequestObjectResult(
                    ResponseApi<object>.BadRequest(context.ModelState)
                );
            }
        }
    }
}