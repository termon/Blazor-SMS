using System;
using FluentValidation;

using SMS.Core.Dtos;
using SMS.Data.Services;

namespace SMS.Rest.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator(IStudentService _svc)
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(20);

            RuleFor(p => p.Email).NotEmpty().EmailAddress();

            RuleFor(p => p.Password).NotEmpty().MinimumLength(3);
            
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password);

            RuleFor(p => p.Role).IsInEnum();
            
            RuleFor(p => p.Email).Custom((email, context) => {
                if ((_svc.GetUserByEmailAddress(email) != null)) {
                    context.AddFailure("Email", "Email has already been registered. Please use another.");
                }
            });            
        }    
    }
}