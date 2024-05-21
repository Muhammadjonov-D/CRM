using CRM.DataAccess.UnitOfWorks;
using CRM.Service.Services.Attendances;
using CRM.Service.Services.Courses;
using CRM.Service.Services.Exams;
using CRM.Service.Services.Groups;
using CRM.Service.Services.Lessons;
using CRM.Service.Services.StudentGroups;
using CRM.Service.Services.Students;
using CRM.Service.Services.Teachers;
using CRM.WebApi.ApiService.Attendances;
using CRM.WebApi.ApiService.Courses;
using CRM.WebApi.ApiService.Exams;
using CRM.WebApi.ApiService.Groups;
using CRM.WebApi.ApiService.Lessons;
using CRM.WebApi.ApiService.StudentGroups;
using CRM.WebApi.ApiService.Students;
using CRM.WebApi.ApiService.Teachers;
using CRM.WebApi.Validators.Attendances;
using CRM.WebApi.Validators.Courses;
using CRM.WebApi.Validators.Exams;
using CRM.WebApi.Validators.Groups;
using CRM.WebApi.Validators.Lessons;
using CRM.WebApi.Validators.StudentGroups;
using CRM.WebApi.Validators.Students;
using CRM.WebApi.Validators.Teachers;

namespace CRM.WebApi.Extensions;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<IStudentGroupService, StudentGroupService>();
    }

    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IExamApiService, ExamApiService>();
        services.AddScoped<IGroupApiService, GroupApiService>();
        services.AddScoped<ILessonApiService, LessonApiService>();
        services.AddScoped<ICourseApiService, CourseApiService>();
        services.AddScoped<IStudentApiService, StudentApiService>();
        services.AddScoped<ITeacherApiService, TeacherApiService>();
        services.AddScoped<IAttendanceApiService, AttendanceApiService>();
        services.AddScoped<IStudentGroupApiService, StudentGroupApiService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<ExamCreateModelValidator>();
        services.AddTransient<ExamUpdateModelValidator>();

        services.AddTransient<GroupCreateModelValidator>();
        services.AddTransient<GroupUpdateModelValidator>();

        services.AddTransient<LessonCreateModelValidator>();
        services.AddTransient<LessonUpdateModelValidator>();

        services.AddTransient<CourseCreateModelValidator>();
        services.AddTransient<CourseUpdateModelValidator>();

        services.AddTransient<StudentCreateModelValidator>();
        services.AddTransient<StudentUpdateModelValidator>();

        services.AddTransient<TeacherCreateModelValidator>();
        services.AddTransient<TeacherUpdateModelValidator>();

        services.AddTransient<AttendanceCreateModelValidator>();
        services.AddTransient<AttendanceUpdateModelValidator>();

        services.AddTransient<StudentGroupUpdateModelValidator>();
        services.AddTransient<StudentGroupUpdateModelValidator>();
    }   
}
