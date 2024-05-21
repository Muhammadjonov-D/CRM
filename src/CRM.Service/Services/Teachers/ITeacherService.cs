using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Teachers;

public interface ITeacherService
{
    ValueTask<Teacher> CreateAsync(Teacher teacher);
    ValueTask<Teacher> UpdateAsync(long id, Teacher teacher);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Teacher> GetByIdAsync(long id);
    ValueTask<IEnumerable<Teacher>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
