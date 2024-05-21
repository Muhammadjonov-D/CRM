using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Exams;

public interface IExamService
{
    ValueTask<Exam> CreateAsync(Exam exam);
    ValueTask<Exam> UpdateAsync(long id, Exam exam);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Exam> GetByIdAsync(long id);
    ValueTask<IEnumerable<Exam>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
