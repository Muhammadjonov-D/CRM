using CRM.WebApi.Models.Teachers;
using FluentValidation;

namespace CRM.WebApi.Validators.Teachers;

public class TeacherCreateModelValidator : AbstractValidator<TeacherCreateModel>
{
    public TeacherCreateModelValidator()
    {
        RuleFor(teacher => teacher.FirstName)
         .NotNull()
         .WithMessage(teacher => $"{nameof(teacher.FirstName)} is not specified");

        RuleFor(teacher => teacher.LastName)
           .NotNull()
           .WithMessage(teacher => $"{nameof(teacher.LastName)} is not specified");

        RuleFor(teacher => teacher.PhoneNumber)
           .NotNull()
           .WithMessage(teacher => $"{nameof(teacher.PhoneNumber)} is not specified");
    }
}
