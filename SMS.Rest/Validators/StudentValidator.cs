using FluentValidation;
using FluentValidation.Results;
using SMS.Core.Dtos;
using SMS.Data.Services;
using System;
using System.Net;

namespace SMS.Rest.Validators
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator(IStudentService _svc)
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Course).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Age).NotEmpty().GreaterThan(16).LessThan(80);   
            RuleFor(p => p.Grade).GreaterThan(-1).LessThan(101).WithMessage("Grade must be between 0 and 100");
            RuleFor(p => p.Email).NotEmpty().EmailAddress().Custom((email, context) => {
                var s = (StudentDto) context.InstanceToValidate;
                if ((_svc.GetStudentByEmail(email, s.Id) != null)) {                   
                    context.AddFailure("Email", "Email has already been registered. Please use another.");
                }
            });
            RuleFor( p => p.PhotoUrl).Custom((url, context) => {
                if (url != null && url != "")
                {
                    try {
                        var uri = new Uri(url, UriKind.Absolute);
                        // using method head doesn't down load the resource, rather it just verifies its existence
                        WebRequest webRequest = WebRequest.Create(uri);
                        webRequest.Method = "HEAD";
                        webRequest.GetResponse();                            
                    } 
                    catch
                    {
                        context.AddFailure("Url Endpoint is not valid");
                    }
                };            
            }); 
        }
    }
}