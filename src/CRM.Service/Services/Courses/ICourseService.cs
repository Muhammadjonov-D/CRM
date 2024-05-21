using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Courses;

public interface ICourseService
{
    ValueTask<Course> CreateAsync(Course course);
    ValueTask<Course> UpdateAsync(long id, Course course);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Course> GetByIdAsync(long id);
    ValueTask<IEnumerable<Course>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
