using FluentValidation;
using FluentValidation.Results;
using SMS.Core.Dtos;
using SMS.Core.Helpers;
using SMS.Data.Services;


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
             
            RuleFor(p => p.PhotoUrl).Must(u => CoreUtils.UrlExists(u)).WithMessage("Url Endpoint is not valid");
           
            // requires custom validator so we can access Student Id
            RuleFor(p => p.Email).NotEmpty().EmailAddress().Custom( (email, context) => {
                var s = (StudentDto) context.InstanceToValidate;
                if ((!_svc.StudentCanUseEmail(s.Email, s.Id))) {                   
                    context.AddFailure("Email has already been registered. Please use another.");
                }
            });
            
        }
    }
}