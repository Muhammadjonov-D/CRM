﻿using CRM.WebApi.Models.Students;
using FluentValidation;

namespace CRM.WebApi.Validators.Students;

public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
{
    public StudentCreateModelValidator()
    {
        RuleFor(student => student.FirstName)
           .NotNull()
           .WithMessage(student => $"{nameof(student.FirstName)} is not specified");

        RuleFor(student => student.LastName)
            .NotNull()
            .WithMessage(student => $"{nameof(student.LastName)} is not specified");
    }
}
