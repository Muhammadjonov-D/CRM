using CRM.WebApi.Models.Exams;
using FluentValidation;

namespace CRM.WebApi.Validators.Exams;

public class ExamUpdateModelValidator : AbstractValidator<ExamUpdateModel>
{
    public ExamUpdateModelValidator()
    {
        RuleFor(exam => exam.Name)
        .NotNull()
        .WithMessage(exam => $"{nameof(exam.Name)} is not specified");

        RuleFor(exam => exam.GroupId)
        .NotNull()
        .WithMessage(exam => $"{nameof(exam.GroupId)} is not specified");
    }
}
