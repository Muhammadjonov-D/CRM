using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class ExamGrade : Auditable
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long ExamId { get; set; }
    public Exam Exam { get; set; }
    public int Answers {  get; set; }
    public int Questions { get; set; }
    public float Grade { get; set; }
}
