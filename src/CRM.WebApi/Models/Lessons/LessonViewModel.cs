namespace CRM.WebApi.Models.Lessons;

public class LessonViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
}
