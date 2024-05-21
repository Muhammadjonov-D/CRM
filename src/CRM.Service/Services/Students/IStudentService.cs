using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Students;

public interface IStudentService
{
    ValueTask<Student> CreateAsync(Student student);
    ValueTask<Student> UpdateAsync(long id, Student student);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Student> GetByIdAsync(long id);
    ValueTask<IEnumerable<Student>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
