using System.Text.RegularExpressions;

namespace CRM.WebApi.Models.Courses;

public class CourseUpdateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}
