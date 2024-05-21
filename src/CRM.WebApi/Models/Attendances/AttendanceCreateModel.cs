using CRM.Domain.Enums;

namespace CRM.WebApi.Models.Attendances;

public class AttendanceCreateModel
{
    public long StudentId { get; set; }
    public AttendanceStatus Status { get; set; }
    public long LessonId { get; set; }
}
