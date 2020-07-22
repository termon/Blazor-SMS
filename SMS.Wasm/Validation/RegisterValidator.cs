using System;
using FluentValidation;
using SMS.Core.Dtos;
using SMS.Wasm.Services;

namespace SMS.Wasm.Validation
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator(AuthService _svc)
        {            
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(20);  

            RuleFor(p => p.Password).NotEmpty()
                .MinimumLength(3);
                        
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password); 

            // uses async validator to call injected AuthService VerifyEmailAvailable action
            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .MustAsync(async (email, cancellation) => {
                     return await _svc.VerifyEmailAvailableAsync(email);
                }).WithMessage("EmailAddress is already Registered"); 
        }    
    }

}