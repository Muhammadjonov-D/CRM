using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class StudentGroup : Auditable
{
    public string Name { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
}
