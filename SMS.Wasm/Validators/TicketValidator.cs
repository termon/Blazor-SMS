using System;
using FluentValidation;
using SMS.Core.Dtos;

namespace SMS.Wasm.Validators
{
    public class TicketValidator : AbstractValidator<TicketDto>
    {
        public TicketValidator()
        {
            RuleFor(t => t.Issue).NotEmpty().MaximumLength(150);
            RuleFor(t => t.StudentId).NotEmpty();            
        }
    }

}