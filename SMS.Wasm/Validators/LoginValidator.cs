using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Wasm.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }    
    }
}