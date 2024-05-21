using CRM.Domain.Entities;

namespace CRM.WebApi.Models.StudentGroups;

public class StudentGroupCreateModel
{
    public string Name { get; set; }
    public long GroupId { get; set; }
    public long StudentId { get; set; }
}
