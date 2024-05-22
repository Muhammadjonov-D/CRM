using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Students;

public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    public async ValueTask<Student> CreateAsync(Student student)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(
            s => s.FirstName.ToLower() == student.FirstName.ToLower()
            && s.LastName.ToLower() == student.LastName.ToLower());
        if (existStudent is not null)
            throw new AlreadyExistException("This student is already exist");

        student.CreatedByUserId = HttpContextHelper.UserId;
        var createdStudent = await unitOfWork.Students.InsertAsync(student);
        await unitOfWork.SaveAsync();

        return createdStudent;
    }

    public async ValueTask<Student> UpdateAsync(long id, Student student)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id && !s.IsDeleted)
             ?? throw new NotFoundException($"Student is not found with this ID = {id}");

        existStudent.FirstName = student.FirstName;
        existStudent.LastName = student.LastName;
        existStudent.PhoneNumber = student.PhoneNumber;

        existStudent.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Students.UpdateAsync(existStudent);
        await unitOfWork.SaveAsync();

        return existStudent;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException($"Student is not found with this ID = {id}");

        await unitOfWork.Students.DeleteAsync(existStudent);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Student> GetByIdAsync(long id)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException($"Student is not found with this ID = {id}");

        return existStudent;
    }

    public async ValueTask<IEnumerable<Student>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var students = unitOfWork.Students
             .SelectAsQueryable(expression: t => !t.IsDeleted, isTracked: false)
             .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            students = students.Where(student =>
               student.FirstName.ToLower().Contains(search.ToLower()) ||
               student.LastName.ToLower().Contains(search.ToLower()));

        return await students.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
