using System.Text.RegularExpressions;

namespace CRM.WebApi.Models.Teachers;

public class TeacherViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}
