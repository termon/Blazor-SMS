using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SMS.Core.Dtos;

namespace SMS.Rest.Models
{
    public static class ResponseApi<T>
    {
        public static  ApiResponse<T> Ok(T result, string msg=null)
        {
            return new ApiResponse<T> { StatusCode = 200, Result = result, Message = msg ?? "Success"};
        }
        public static  ApiResponse NoContent(string msg=null)
        {
            return new ApiResponse { StatusCode = 204, Message = msg ?? "No content" };
        }
        public static  ApiResponse<T> Created(T result, string msg=null)
        {
            return new ApiResponse<T> { StatusCode = 201, Result = result, Message = msg ?? "Created" };
        }
        public static  ApiResponse NotFound(string msg=null)
        {
            return new ApiResponse { StatusCode = 404, Message = msg ?? "Not found" };
        }
        public static  ApiResponse BadRequest(string msg=null)
        {
            return new ApiResponse { StatusCode = 400, Message = msg ?? "Bad request" };
        }
        public static  ApiResponse NotAuthorised(string msg=null)
        {
            return new ApiResponse { StatusCode = 401, Message = msg ?? "Not authorised" };
        }

        public static  ApiResponse BadRequest(ModelStateDictionary modelState)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }
            return new ApiResponse {                
                StatusCode = 400,
                Message = "Validation Errors",
                Errors = modelState.SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray(),                
            };   
        }
    }

}