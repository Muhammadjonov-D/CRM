using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.StudentGroups;

public interface IStudentGroupService
{
    ValueTask<StudentGroup> CreateAsync(StudentGroup studentGroup);
    ValueTask<StudentGroup> UpdateAsync(long id, StudentGroup studentGroup);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<StudentGroup> GetByIdAsync(long id);
    ValueTask<IEnumerable<StudentGroup>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
