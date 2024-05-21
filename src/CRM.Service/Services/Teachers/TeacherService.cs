using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Teachers;

public class TeacherService(IUnitOfWork unitOfWork) : ITeacherService
{
    public async ValueTask<Teacher> CreateAsync(Teacher teacher)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(
            t => t.FirstName.ToLower() == teacher.FirstName.ToLower() 
            && t.LastName.ToLower() == teacher.LastName.ToLower());
        if (existTeacher is not null)
            throw new AlreadyExistException("This teacher is already exist");

        teacher.CreatedByUserId = HttpContextHelper.UserId;
        var createdTeacher = await unitOfWork.Teachers.InsertAsync(teacher);
        await unitOfWork.SaveAsync();

        return createdTeacher;
    }

    public async ValueTask<Teacher> UpdateAsync(long id, Teacher teacher)
    {
       var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Teacher is not found with this ID = {id}");

        existTeacher.FirstName = teacher.FirstName;
        existTeacher.LastName = teacher.LastName;
        existTeacher.PhoneNumber = teacher.PhoneNumber;
        existTeacher.Password = teacher.Password;

        existTeacher.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Teachers.UpdateAsync(existTeacher);
        await unitOfWork.SaveAsync();

        return existTeacher;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id && !t.IsDeleted)
            ?? throw new NotFoundException($"Teacher is not found with this ID = {id}");

        await unitOfWork.Teachers.DeleteAsync(existTeacher);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Teacher> GetByIdAsync(long id)
    {
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id && !t.IsDeleted)
             ?? throw new NotFoundException($"Teacher is not found with this ID = {id}");

        return existTeacher;
    }

    public async ValueTask<IEnumerable<Teacher>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var teachers = unitOfWork.Teachers
             .SelectAsQueryable(expression: t => !t.IsDeleted, isTracked: false)
             .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            teachers = teachers.Where(teacher =>
               teacher.FirstName.ToLower().Contains(search.ToLower()) ||
               teacher.LastName.ToLower().Contains(search.ToLower()));

       return await teachers.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
