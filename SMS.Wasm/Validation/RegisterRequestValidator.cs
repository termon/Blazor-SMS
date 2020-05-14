using System;
using FluentValidation;
using SMS.Core.Dtos;
using SMS.Wasm.Services;

namespace SMS.Wasm.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {

        public RegisterRequestValidator(AuthService _svc)
        {            
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(20);  

            RuleFor(p => p.Password).NotEmpty()
                .MinimumLength(3);
                        
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password); 

            RuleFor(p => p.EmailAddress).NotEmpty()
                .EmailAddress()
                // async validators currently cause browser lockup
                // .Custom(async (email,context) => {               
                //     var available = await _svc.VerifyEmailAvailableAsync(email);
                //     if (!available)
                //     {
                //         Console.WriteLine("Adding Email Validation Error Message");
                //         context.AddFailure("Email has already been registered. Please use another.");     
                //     }    
                // })
                ; 
        }    
    }


}