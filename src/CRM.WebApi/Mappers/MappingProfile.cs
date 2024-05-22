using AutoMapper;
using CRM.Domain.Entities;
using CRM.WebApi.Models.Attendances;
using CRM.WebApi.Models.Courses;
using CRM.WebApi.Models.Exams;
using CRM.WebApi.Models.Groups;
using CRM.WebApi.Models.Lessons;
using CRM.WebApi.Models.StudentGroups;
using CRM.WebApi.Models.Students;
using CRM.WebApi.Models.Teachers;

namespace CRM.WebApi.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AttendanceViewModel, Attendance>().ReverseMap();
        CreateMap<Attendance, AttendanceCreateModel>().ReverseMap();
        CreateMap<Attendance, AttendanceUpdateModel>().ReverseMap();

        CreateMap<CourseViewModel, Course>().ReverseMap();
        CreateMap<Course, CourseCreateModel>().ReverseMap();
        CreateMap<Course, CourseUpdateModel>().ReverseMap();

        CreateMap<ExamViewModel, Exam>().ReverseMap();
        CreateMap<Exam, ExamCreateModel>().ReverseMap();
        CreateMap<Exam, ExamUpdateModel>().ReverseMap();

        CreateMap<GroupViewModel, Group>().ReverseMap();
        CreateMap<Group, GroupCreateModel>().ReverseMap();
        CreateMap<Group, GroupUpdateModel>().ReverseMap();

        CreateMap<LessonViewModel, Lesson>().ReverseMap();
        CreateMap<Lesson, LessonCreateModel>().ReverseMap();
        CreateMap<Lesson, LessonUpdateModel>().ReverseMap();

        CreateMap<StudentGroupViewModel, StudentGroup>().ReverseMap();
        CreateMap<StudentGroup, StudentGroupCreateModel>().ReverseMap();
        CreateMap<StudentGroup, StudentGroupUpdateModel>().ReverseMap();

        CreateMap<StudentViewModel, Student>().ReverseMap();
        CreateMap<Student, StudentCreateModel>().ReverseMap();
        CreateMap<Student, StudentUpdateModel>().ReverseMap();

        CreateMap<TeacherViewModel, Teacher>().ReverseMap();
        CreateMap<Teacher, TeacherCreateModel>().ReverseMap();
        CreateMap<Teacher, TeacherUpdateModel>().ReverseMap();
    }
}
