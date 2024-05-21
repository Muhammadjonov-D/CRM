using CRM.Service.Configurations;
using CRM.WebApi.Models.Courses;

namespace CRM.WebApi.ApiService.Courses;

public interface ICourseApiService
{
    ValueTask<CourseViewModel> PostAsync(CourseCreateModel createModel);
    ValueTask<CourseViewModel> PutAsync(long id, CourseUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CourseViewModel> GetAsync(long id);
    ValueTask<IEnumerable<CourseViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
