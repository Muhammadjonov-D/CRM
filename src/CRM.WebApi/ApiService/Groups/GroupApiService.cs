using AutoMapper;
using CRM.Service.Configurations;
using CRM.Service.Services.Groups;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Groups;
using CRM.WebApi.Validators.Groups;
using System.Text.RegularExpressions;

namespace CRM.WebApi.ApiService.Groups;

public class GroupApiService
    (IMapper mapper,
    IGroupService groupService,
    GroupCreateModelValidator createModelValidator,
    GroupUpdateModelValidator updateModelValidator): IGroupApiService
{
    public async ValueTask<GroupViewModel> PostAsync(GroupCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedGroup = mapper.Map<Group>(createModel);
        var createdGroup = await groupService.CreateAsync(mappedGroup);
        return mapper.Map<GroupViewModel>(createdGroup);
    }

    public async ValueTask<GroupViewModel> PutAsync(long id, GroupUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedGroup = mapper.Map<Group>(updateModel);
        var updatedGroup = await groupService.UpdateAsync(id, mappedGroup);
        return mapper.Map<GroupViewModel>(updatedGroup);
    }
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await groupService.DeleteAsync(id);
    }

    public async ValueTask<GroupViewModel> GetAsync(long id)
    {
        var group = await groupService.GetByIdAsync(id);
        return mapper.Map<GroupViewModel>(group);
    }

    public async ValueTask<IEnumerable<GroupViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var groups = await groupService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<GroupViewModel>>(groups);
    }
}
