using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class Lesson : Auditable
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
}
