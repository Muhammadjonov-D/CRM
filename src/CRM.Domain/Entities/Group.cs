using CRM.Domain.Commons;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities;

public class Group : Auditable
{
    public string Name { get; set; }
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
    public GroupStatus Status { get; set; }
}
