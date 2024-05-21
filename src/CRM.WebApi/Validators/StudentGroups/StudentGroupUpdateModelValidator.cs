using CRM.WebApi.Models.StudentGroups;
using FluentValidation;

namespace CRM.WebApi.Validators.StudentGroups;

public class StudentGroupUpdateModelValidator : AbstractValidator<StudentGroupUpdateModel>
{
    public StudentGroupUpdateModelValidator()
    {
        RuleFor(sg => sg.Name)
           .NotNull()
           .WithMessage(sg => $"{nameof(sg.Name)} is not specified");

        RuleFor(sg => sg.GroupId)
            .NotNull()
            .WithMessage(sg => $"{nameof(sg.GroupId)} is not specified");

        RuleFor(sg => sg.StudentId)
            .NotNull()
            .WithMessage(sg => $"{nameof(sg.StudentId)} is not specified");
    }
}
