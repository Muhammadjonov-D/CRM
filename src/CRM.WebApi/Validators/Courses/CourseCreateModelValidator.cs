using CRM.Domain.Entities;
using CRM.WebApi.Models.Courses;
using FluentValidation;

namespace CRM.WebApi.Validators.Courses;

public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator()
    {
        RuleFor(course => course.Name)
            .NotNull()
            .WithMessage(course => $"{nameof(course.Name)} is not specified");

        RuleFor(course => course.Price)
            .NotNull()
            .WithMessage(course => $"{nameof(course.Price)} is not specified");
    }
}
