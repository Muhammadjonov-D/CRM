using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Exceptions;
using CRM.Service.Extensions;
using CRM.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CRM.Service.Services.Groups;

public class GroupService(IUnitOfWork unitOfWork) : IGroupService
{
    public async ValueTask<Group> CreateAsync(Group group)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Name.ToLower() == group.Name.ToLower());
        if (existGroup is not null)
            throw new AlreadyExistException("This group already exists");

        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == group.TeacherId)
           ?? throw new NotFoundException($"Teacher is not found with this ID = {group.TeacherId}");

        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == group.CourseId)
           ?? throw new NotFoundException($"Course is not found with this ID = {group.CourseId}");

        group.CreatedByUserId = HttpContextHelper.UserId;
        var createdGroup = await unitOfWork.Groups.InsertAsync(group);
        await unitOfWork.SaveAsync();

        return createdGroup;
    }

    public async ValueTask<Group> UpdateAsync(long id, Group group)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == id && !g.IsDeleted)
            ?? throw new NotFoundException($"Group is not found with this ID = {id}");

        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == group.TeacherId)
         ?? throw new NotFoundException($"Teacher is not found with this ID = {group.TeacherId}");

        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == group.CourseId)
           ?? throw new NotFoundException($"Course is not found with this ID = {group.CourseId}");

        existGroup.Name = group.Name;
        existGroup.Status = group.Status;

        existGroup.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Groups.UpdateAsync(existGroup);
        await unitOfWork.SaveAsync();
        existGroup.Teacher = existTeacher;
        existGroup.Course = existCourse;

        return existGroup;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existGroup = await unitOfWork.Groups.SelectAsync(g => g.Id == id && !g.IsDeleted)
           ?? throw new NotFoundException($"Group is not found with this ID = {id}");

        existGroup.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Groups.DeleteAsync(existGroup);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Group> GetByIdAsync(long id)
    {
        var existGroup = await unitOfWork.Groups
            .SelectAsync(expression: g => g.Id == id && !g.IsDeleted, includes: ["Teacher", "Course"])
            ?? throw new NotFoundException($"Group is not found with this Id = {id}");

        return existGroup;
    }

    public async ValueTask<IEnumerable<Group>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var groups = unitOfWork.Groups
           .SelectAsQueryable(expression: g => !g.IsDeleted, includes: ["Teacher", "Course"], isTracked: false)
           .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            groups = groups.Where(group =>
            group.Name.ToLower().Contains(search.ToLower()));

        return await groups.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
