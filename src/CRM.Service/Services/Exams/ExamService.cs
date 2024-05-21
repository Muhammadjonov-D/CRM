using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Exams;

public class ExamService(IUnitOfWork unitOfWork) : IExamService
{
    public async ValueTask<Exam> CreateAsync(Exam exam)
    {
        var existExam = await unitOfWork.Exams.SelectAsync(e => e.Name.ToLower() == exam.Name.ToLower());
        if (existExam is not null)
            throw new AlreadyExistException("Exam is already exist");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == exam.GroupId)
           ?? throw new NotFoundException($"Group is not found with this ID = {exam.GroupId}");

        exam.CreatedByUserId = HttpContextHelper.UserId;
        var createdExam = await unitOfWork.Exams.InsertAsync(exam);
        await unitOfWork.SaveAsync();

        return createdExam;
    }

    public async ValueTask<Exam> UpdateAsync(long id, Exam exam)
    {
        var existExam = await unitOfWork.Exams.SelectAsync(e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Exam is not found with this ID = {id}");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == exam.GroupId)
           ?? throw new NotFoundException($"Group is not found with this ID = {exam.GroupId}");

        existExam.Name = exam.Name;
        existExam.GroupId = exam.GroupId;
        existExam.ExamREsult = exam.ExamREsult;

        existExam.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Exams.UpdateAsync(existExam);
        await unitOfWork.SaveAsync();

        return existExam;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existExam = await unitOfWork.Exams.SelectAsync(e => e.Id == id && !e.IsDeleted)
            ?? throw new NotFoundException($"Exam is not found with this ID = {id}");

        await unitOfWork.Exams.DeleteAsync(existExam);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Exam> GetByIdAsync(long id)
    {
        var existExam = await unitOfWork.Exams.SelectAsync(e => e.Id == id && !e.IsDeleted, ["Group"])
            ?? throw new NotFoundException($"Exam is not found with this ID = {id}");

        return existExam;
    }

    public async ValueTask<IEnumerable<Exam>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var exams = unitOfWork.Exams
              .SelectAsQueryable(expression: exam => !exam.IsDeleted, includes: ["Group"], isTracked: false)
              .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            exams = exams.Where(exam =>
                exam.Name.ToLower().Contains(search.ToLower()));

        return await exams.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
