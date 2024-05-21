using CRM.WebApi.Models.Courses;
using FluentValidation;

namespace CRM.WebApi.Validators.Courses;

public class CourseUpdateModelValidator : AbstractValidator<CourseUpdateModel>
{
    public CourseUpdateModelValidator()
    {
        RuleFor(course => course.Name)
            .NotNull()
            .WithMessage(course => $"{nameof(course.Name)} is not specified");

        RuleFor(course => course.Price)
            .NotNull()
            .WithMessage(course => $"{nameof(course.Price)} is not specified");
    }
}
