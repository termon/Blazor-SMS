using System;
using FluentValidation;
using SMS.Core.Dtos;

namespace SMS.Wasm.Validation
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Course).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Age).NotEmpty().GreaterThan(15).LessThan(80);
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Grade).GreaterThan(-1).LessThan(101).WithMessage("Grade must be between 0 and 100");
        }
    }

}