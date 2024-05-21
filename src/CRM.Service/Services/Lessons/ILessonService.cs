using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Lessons;

public interface ILessonService
{
    ValueTask<Lesson> CreateAsync(Lesson lesson);
    ValueTask<Lesson> UpdateAsync(long id, Lesson lesson);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Lesson> GetByIdAsync(long id);
    ValueTask<IEnumerable<Lesson>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
