using CRM.Service.Configurations;
using CRM.WebApi.Models.Teachers;

namespace CRM.WebApi.ApiService.Teachers;

public interface ITeacherApiService
{
    ValueTask<TeacherViewModel> PostAsync(TeacherCreateModel createModel);
    ValueTask<TeacherViewModel> PutAsync(long id, TeacherUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TeacherViewModel> GetAsync(long id);
    ValueTask<IEnumerable<TeacherViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
