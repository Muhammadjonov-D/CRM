using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class Payment : Auditable
{
    public long StudentGroupId { get; set; }
    public StudentGroup StudentGroup { get; set; }
    public decimal Paid {  get; set; }
}
