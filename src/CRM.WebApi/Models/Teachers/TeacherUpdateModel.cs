using System.Text.RegularExpressions;

namespace CRM.WebApi.Models.Teachers;

public class TeacherUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}
