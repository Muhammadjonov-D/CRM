using CRM.Service.Configurations;
using CRM.WebApi.Models.StudentGroups;

namespace CRM.WebApi.ApiService.StudentGroups;

public interface IStudentGroupApiService
{
    ValueTask<StudentGroupViewModel> PostAsync(StudentGroupCreateModel createModel);
    ValueTask<StudentGroupViewModel> PutAsync(long id, StudentGroupUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<StudentGroupViewModel> GetAsync(long id);
    ValueTask<IEnumerable<StudentGroupViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
