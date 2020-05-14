using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Rest.Validators
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