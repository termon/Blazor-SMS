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

            RuleFor(p => p.Password).NotEmpty().MinimumLength(3);
            
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password);

            RuleFor(p => p.Role).IsInEnum();
            
            // can use Must instead of custom validator as we do not need access to student Id
            RuleFor(p => p.Email).NotEmpty()
                                 .EmailAddress()
                                 .Must(e => _svc.GetUserByEmailAddress(e) == null)
                                 .WithMessage("Email has already been registered. Please use another.");           
        }    
    }
}