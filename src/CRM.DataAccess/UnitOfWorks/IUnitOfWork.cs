using CRM.DataAccess.Repositories;
using CRM.Domain.Entities;

namespace CRM.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Attendance> Attendances { get; }
    IRepository<Course> Courses { get; }
    IRepository<Exam> Exams { get; }
    IRepository<Group> Groups { get; }
    IRepository<Lesson> Lessons { get; }
    IRepository<Student> Students { get; }
    IRepository<StudentGroup> StudentGroups { get; }
    IRepository<Teacher> Teachers { get; }
    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
}
