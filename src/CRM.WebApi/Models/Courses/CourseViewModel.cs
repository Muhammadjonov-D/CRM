using System.Text.RegularExpressions;

namespace CRM.WebApi.Models.Courses;

public class CourseViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}
