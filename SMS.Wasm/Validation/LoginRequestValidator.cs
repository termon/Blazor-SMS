using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Wasm.Validation
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