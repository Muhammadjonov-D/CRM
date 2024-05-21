using CRM.WebApi.Models.Groups;
using FluentValidation;

namespace CRM.WebApi.Validators.Groups;

public class GroupUpdateModelValidator : AbstractValidator<GroupUpdateModel>
{
    public GroupUpdateModelValidator()
    {
        RuleFor(group => group.Name)
            .NotNull()
            .WithMessage(group => $"{nameof(group.Name)} is not specified");

        RuleFor(group => group.TeacherId)
            .NotNull()
            .WithMessage(group => $"{nameof(group.TeacherId)} is not specified");

        RuleFor(group => group.CourseId)
            .NotNull()
            .WithMessage(group => $"{nameof(group.CourseId)} is not specified");
    }
}
