using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class Course : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
