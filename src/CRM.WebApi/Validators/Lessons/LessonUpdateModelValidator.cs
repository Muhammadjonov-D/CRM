using CRM.WebApi.Models.Lessons;
using FluentValidation;

namespace CRM.WebApi.Validators.Lessons;

public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator()
    {
        RuleFor(lesson => lesson.Name)
           .NotNull()
           .WithMessage(lesson => $"{nameof(lesson.Name)} is not specified");

        RuleFor(lesson => lesson.GroupId)
            .NotNull()
            .WithMessage(lesson => $"{nameof(lesson.GroupId)} is not specified");
    }
}
