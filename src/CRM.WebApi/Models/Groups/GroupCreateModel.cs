using CRM.Domain.Enums;

namespace CRM.WebApi.Models.Groups;

public class GroupCreateModel
{
    public string Name { get; set; }
    public long TeacherId { get; set; }
    public long CourseId { get; set; }
    public GroupStatus Status { get; set; }
}
