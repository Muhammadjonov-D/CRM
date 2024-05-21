using CRM.WebApi.Models.Attendances;
using FluentValidation;

namespace CRM.WebApi.Validators.Attendances;

public class AttendanceUpdateModelValidator : AbstractValidator<AttendanceUpdateModel>
{
    public AttendanceUpdateModelValidator()
    {
        RuleFor(attendance => attendance.StudentId)
            .NotNull()
            .WithMessage(attendance => $"{nameof(attendance.StudentId)} is not specified");

        RuleFor(attendance => attendance.LessonId)
            .NotNull()
            .WithMessage(attendance => $"{nameof(attendance.LessonId)} is not specified");
    }
}
