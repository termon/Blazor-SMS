using FluentValidation;
using SMS.Core.Dtos;

namespace SMS.Rest.Validators
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketValidator()
        {
            RuleFor(p => p.StudentId).NotEmpty();
            RuleFor(p => p.Issue).NotEmpty().MaximumLength(50);
        }
    }
}