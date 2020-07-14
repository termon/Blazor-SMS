using FluentValidation;

using SMS.Core.Dtos;

namespace SMS.Rest.Validators
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