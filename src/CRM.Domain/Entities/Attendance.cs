using CRM.Domain.Commons;
using CRM.Domain.Enums;
using System.Text.RegularExpressions;

namespace CRM.Domain.Entities;

public class Attendance : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public AttendanceStatus Status { get; set; }
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }
}
