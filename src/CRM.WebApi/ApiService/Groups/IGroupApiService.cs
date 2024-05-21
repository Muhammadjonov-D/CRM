using CRM.Service.Configurations;
using CRM.WebApi.Models.Groups;

namespace CRM.WebApi.ApiService.Groups;

public interface IGroupApiService
{
    ValueTask<GroupViewModel> PostAsync(GroupCreateModel createModel);
    ValueTask<GroupViewModel> PutAsync(long id, GroupUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<GroupViewModel> GetAsync(long id);
    ValueTask<IEnumerable<GroupViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
