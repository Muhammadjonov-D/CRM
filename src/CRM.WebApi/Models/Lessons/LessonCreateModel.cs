namespace CRM.WebApi.Models.Lessons;

public class LessonCreateModel
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
}
