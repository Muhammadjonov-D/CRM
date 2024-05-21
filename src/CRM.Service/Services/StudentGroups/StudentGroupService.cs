using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.StudentGroups;

public class StudentGroupService(IUnitOfWork unitOfWork) : IStudentGroupService
{
    public async ValueTask<StudentGroup> CreateAsync(StudentGroup studentGroup)
    {
        var existStudentGroup = await unitOfWork.StudentGroups.SelectAsync(s => s.Name.ToLower() == studentGroup.Name.ToLower())
            ?? throw new AlreadyExistException("StudentGroup is already exist");

        var existStudent = await unitOfWork.Students.SelectAsync(t => t.Id == studentGroup.StudentId)
           ?? throw new NotFoundException($"Student is not found with this ID = {studentGroup.StudentId}");

        var existGroup = await unitOfWork.Groups.SelectAsync(c => c.Id == studentGroup.GroupId)
           ?? throw new NotFoundException($"Group is not found with this ID = {studentGroup.GroupId}");

        if (existStudentGroup is not null)
            throw new AlreadyExistException($"StudentGroup is already exists");

        studentGroup.CreatedByUserId = HttpContextHelper.UserId;

        var createdStudentGroup = await unitOfWork.StudentGroups.InsertAsync(studentGroup);
        createdStudentGroup.Student = existStudent;
        createdStudentGroup.Group = existGroup;
        await unitOfWork.SaveAsync();

        return createdStudentGroup;
    }

    public async ValueTask<StudentGroup> UpdateAsync(long id, StudentGroup studentGroup)
    {
        var existStudentGroup = await unitOfWork.StudentGroups.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException($"StudentGroup is not found with this ID = {id}");

        var existStudent = await unitOfWork.Students.SelectAsync(t => t.Id == studentGroup.StudentId)
          ?? throw new NotFoundException($"Student is not found with this ID = {studentGroup.StudentId}");

        var existGroup = await unitOfWork.Groups.SelectAsync(c => c.Id == studentGroup.GroupId)
           ?? throw new NotFoundException($"Group is not found with this ID = {studentGroup.GroupId}");

        existStudentGroup.Name = studentGroup.Name;
        existStudentGroup.StudentId = studentGroup.Id;
        existStudentGroup.GroupId = studentGroup.GroupId;

        existStudentGroup.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.StudentGroups.UpdateAsync(existStudentGroup);
        await unitOfWork.SaveAsync();

        existStudentGroup.Group = existGroup;
        existStudentGroup.Student = existStudent;

        return existStudentGroup;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existStudentGroup = await unitOfWork.StudentGroups.SelectAsync(s => s.Id == id && !s.IsDeleted)
            ?? throw new NotFoundException($"StudentGroup is not found with this ID = {id}");

        await unitOfWork.StudentGroups.DeleteAsync(existStudentGroup);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<StudentGroup> GetByIdAsync(long id)
    {
        var existStudentGroup = await unitOfWork.StudentGroups.SelectAsync(expression: s => s.Id == id && !s.IsDeleted, includes: ["Student", "Group"])
             ?? throw new NotFoundException($"StudentGroup is not found with this ID = {id}");

        return existStudentGroup;
    }

    public async ValueTask<IEnumerable<StudentGroup>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var studentGroups = unitOfWork.StudentGroups
               .SelectAsQueryable(expression: sg => !sg.IsDeleted, includes: ["Group", "Student"], isTracked: false)
               .OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            studentGroups = studentGroups.Where(studentGroup =>
                studentGroup.Name.ToLower().Contains(search.ToLower()));

        return await studentGroups.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
