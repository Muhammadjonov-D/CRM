using CRM.DataAccess.DbContexts;
using CRM.DataAccess.Repositories;
using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRM.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IRepository<Attendance> Attendances { get; }

    public IRepository<Course> Courses { get; }

    public IRepository<Exam> Exams { get; }

    public IRepository<Group> Groups { get; }

    public IRepository<Lesson> Lessons { get; }

    public IRepository<Student> Students { get; }

    public IRepository<StudentGroup> StudentGroups { get; }

    public IRepository<Teacher> Teachers { get; }
    private IDbContextTransaction transaction;

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Attendances = new Repository<Attendance>(this.context);
        Courses = new Repository<Course>(this.context);
        Exams = new Repository<Exam>(this.context);
        Groups = new Repository<Group>(this.context);
        Lessons = new Repository<Lesson>(this.context);
        Students = new Repository<Student>(this.context);
        StudentGroups = new Repository<StudentGroup>(this.context);
        Teachers = new Repository<Teacher>(this.context);
    }

    public async ValueTask BeginTransactionAsync()
    {
        transaction = await this.context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
