using CRM.Service.Configurations;
using CRM.WebApi.Models.Exams;

namespace CRM.WebApi.ApiService.Exams;

public interface IExamApiService
{
    ValueTask<ExamViewModel> PostAsync(ExamCreateModel createModel);
    ValueTask<ExamViewModel> PutAsync(long id, ExamUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ExamViewModel> GetAsync(long id);
    ValueTask<IEnumerable<ExamViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
