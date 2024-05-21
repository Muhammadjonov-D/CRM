using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Courses;

public class CourseService(IUnitOfWork unitOfWork) : ICourseService
{
    public async ValueTask<Course> CreateAsync(Course course)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Name.ToLower() == course.Name.ToLower());
        if (existCourse is not null)
            throw new AlreadyExistException("Course is already exist");

        existCourse.CreatedByUserId = HttpContextHelper.UserId;
        var createdCourse = await unitOfWork.Courses.InsertAsync(existCourse);
        await unitOfWork.SaveAsync();

        return createdCourse;
    }
    public async ValueTask<Course> UpdateAsync(long id, Course course)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == id && !c.IsDeleted)
            ?? throw new NotFoundException($"Course is not found with this ID = {id}");

        existCourse.Name = course.Name;
        existCourse.Description = course.Description;
        existCourse.Price = course.Price;
        existCourse.Groups = course.Groups;

        existCourse.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Courses.UpdateAsync(existCourse);
        await unitOfWork.SaveAsync();

        return existCourse;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == id && !c.IsDeleted)
              ?? throw new NotFoundException($"Course is not found with this ID = {id}");

        await unitOfWork.Courses.DeleteAsync(existCourse);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Course> GetByIdAsync(long id)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == id && !c.IsDeleted)
             ?? throw new NotFoundException($"Course is not found with this ID = {id}");

        return existCourse;
    }

    public async ValueTask<IEnumerable<Course>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var courses = unitOfWork.Courses
              .SelectAsQueryable(expression: t => !t.IsDeleted, isTracked: false)
              .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            courses = courses.Where(course =>
               course.Name.ToLower().Contains(search.ToLower()) ||
               course.Description.ToLower().Contains(search.ToLower()));

        return await courses.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
