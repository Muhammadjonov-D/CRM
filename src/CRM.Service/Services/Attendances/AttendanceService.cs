using CRM.DataAccess.UnitOfWorks;
using CRM.Domain.Entities;
using CRM.Service.Exceptions;
using CRM.Service.Helpers;

namespace CRM.Service.Services.Attendances;

public class AttendanceService(IUnitOfWork unitOfWork) : IAttendanceService
{
    public async ValueTask<Attendance> CreateAsync(Attendance attendance)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(t => t.Id == attendance.StudentId)
          ?? throw new NotFoundException($"Student is not found with this ID = {attendance.StudentId}");

        var existLesson = await unitOfWork.Lessons.SelectAsync(c => c.Id == attendance.LessonId)
           ?? throw new NotFoundException($"Lesson is not found with this ID = {attendance.LessonId}");

        var existAttandance = await unitOfWork.Attendances.SelectAsync(
            a => a.StudentId == attendance.StudentId &&
            a.LessonId == attendance.LessonId && !a.IsDeleted);

        if (existAttandance is not null)
            throw new AlreadyExistException($"Attendance is already exists");

        attendance.CreatedByUserId = HttpContextHelper.UserId;

        var createdAttendance = await unitOfWork.Attendances.InsertAsync(attendance);
        createdAttendance.Student = existStudent;
        createdAttendance.Lesson = existLesson;
        await unitOfWork.SaveAsync();

        return createdAttendance;
    }

    public async ValueTask<Attendance> UpdateAsync(long id, Attendance attendance)
    {
        var existAttendace = await unitOfWork.Attendances.SelectAsync(a => a.Id == id && !a.IsDeleted)
             ?? throw new NotFoundException($"Attendance is not found with this ID = {id}");

        existAttendace.StudentId = attendance.StudentId;
        existAttendace.Lesson = attendance.Lesson;
        existAttendace.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Attendances.UpdateAsync(attendance);
        await unitOfWork.SaveAsync();

        return existAttendace;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == id && !c.IsDeleted)
           ?? throw new NotFoundException($"Course is not found with this ID = {id}");

        await unitOfWork.Courses.DeleteAsync(existCourse);
        await unitOfWork.SaveAsync();
        return true;
    }
}
