using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Wasm.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }    
    }
}