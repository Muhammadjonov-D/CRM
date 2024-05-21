using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Lessons;

public class LessonService(IUnitOfWork unitOfWork) : ILessonService
{
    public async ValueTask<Lesson> CreateAsync(Lesson lesson)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Name.ToLower() == lesson.Name.ToLower());
        if (existLesson is not null)
            throw new AlreadyExistException("Lesson is already exist");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == lesson.GroupId)
            ?? throw new NotFoundException($"Group is not found with this ID = {lesson.GroupId}");

        lesson.CreatedByUserId = HttpContextHelper.UserId;
        var createdLesson = await unitOfWork.Lessons.InsertAsync(lesson);
        await unitOfWork.SaveAsync();

        return  createdLesson;
    }

    public async ValueTask<Lesson> UpdateAsync(long id, Lesson lesson)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id && !l.IsDeleted)
            ?? throw new NotFoundException($"Lesson is not found with this ID = {id}");

        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == lesson.GroupId)
           ?? throw new NotFoundException($"Group is not found with this ID = {lesson.GroupId}");

        existLesson.Name = lesson.Name;
        existLesson.Date = lesson.Date;
        existLesson.GroupId = lesson.GroupId;

        existLesson.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Lessons.UpdateAsync(lesson);
        await unitOfWork.SaveAsync();

        return existLesson;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id && !l.IsDeleted)
              ?? throw new NotFoundException($"Lesson is not found with this ID = {id}");

        await unitOfWork.Lessons.DeleteAsync(existLesson);
        await unitOfWork.SaveAsync();
        return true;    
    }

    public async ValueTask<Lesson> GetByIdAsync(long id)
    {
        var existCourse = await unitOfWork.Lessons.SelectAsync(l => l.Id == id && !l.IsDeleted, ["Group"])
            ?? throw new NotFoundException($"Lesson is not found with this ID={id}");

        return existCourse;
    }

    public async ValueTask<IEnumerable<Lesson>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var lessons = unitOfWork.Lessons
              .SelectAsQueryable(expression: lesson => !lesson.IsDeleted, includes: ["Group"], isTracked: false)
              .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            lessons = lessons.Where(lesson =>
                lesson.Name.ToLower().Contains(search.ToLower()));

        return await lessons.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
