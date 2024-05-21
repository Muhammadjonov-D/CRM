using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class Exam : Auditable
{
    public string Name { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
}
