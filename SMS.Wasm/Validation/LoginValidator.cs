using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Wasm.Validation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(p => p.EmailAddress).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }    
    }
}