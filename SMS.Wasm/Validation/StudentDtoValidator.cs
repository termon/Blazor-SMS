using System;
using FluentValidation;
using SMS.Core.Dtos;
using SMS.Wasm.Services;

namespace SMS.Wasm.Validation
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Course).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Age).NotEmpty().GreaterThan(16).LessThan(80);
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Grade).GreaterThan(-1).LessThan(101).WithMessage("Grade must be between 0 and 100");
        }
    }

}