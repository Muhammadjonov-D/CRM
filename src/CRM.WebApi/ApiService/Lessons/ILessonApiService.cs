using CRM.Service.Configurations;
using CRM.WebApi.Models.Lessons;

namespace CRM.WebApi.ApiService.Lessons;

public interface ILessonApiService
{
    ValueTask<LessonViewModel> PostAsync(LessonCreateModel createModel);
    ValueTask<LessonViewModel> PutAsync(long id, LessonUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<LessonViewModel> GetAsync(long id);
    ValueTask<IEnumerable<LessonViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
