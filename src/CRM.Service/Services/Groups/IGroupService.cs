using CRM.Domain.Entities;
using CRM.Service.Configurations;

namespace CRM.Service.Services.Groups;

public interface IGroupService
{
    ValueTask<Group> CreateAsync(Group group);
    ValueTask<Group> UpdateAsync(long id, Group group);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Group> GetByIdAsync(long id);
    ValueTask<IEnumerable<Group>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
