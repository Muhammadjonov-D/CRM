using CRM.Service.Configurations;
using CRM.WebApi.Models.Students;

namespace CRM.WebApi.ApiService.Students;

public interface IStudentApiService
{
    ValueTask<StudentViewModel> PostAsync(StudentCreateModel createModel);
    ValueTask<StudentViewModel> PutAsync(long id, StudentUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<StudentViewModel> GetAsync(long id);
    ValueTask<IEnumerable<StudentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
