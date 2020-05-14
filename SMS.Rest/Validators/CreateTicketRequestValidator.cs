using FluentValidation;
using SMS.Core.Dtos;

namespace SMS.Rest.Validators
{
    public class TicketValidator : AbstractValidator<CreateTicketRequest>
    {
        public TicketValidator()
        {
            RuleFor(p => p.StudentId).NotEmpty();
            RuleFor(p => p.Issue).NotEmpty().MaximumLength(50);
        }
    }
}