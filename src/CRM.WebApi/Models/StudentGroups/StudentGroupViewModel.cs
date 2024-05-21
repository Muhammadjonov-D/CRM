namespace CRM.WebApi.Models.StudentGroups;

public class StudentGroupViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long GroupId { get; set; }
    public long StudentId { get; set; }
}
