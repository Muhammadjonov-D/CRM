using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CRM.DataAccess.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Exam> Exams { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<ExamGrade> ExamGrades { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }
}
