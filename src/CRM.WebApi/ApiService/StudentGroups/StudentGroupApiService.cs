using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.StudentGroups;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.StudentGroups;
using CRM.WebApi.Validators.StudentGroups;

namespace CRM.WebApi.ApiService.StudentGroups;

public class StudentGroupApiService
    (IMapper mapper,
    IStudentGroupService studentGroupService,
    StudentGroupCreateModelValidator createModelValidator,
    StudentGroupUpdateModelValidator updateModelValidator) : IStudentGroupApiService
{
    public async ValueTask<StudentGroupViewModel> PostAsync(StudentGroupCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedStudentGroup = mapper.Map<StudentGroup>(createModel);
        var createdStudentGroup = await studentGroupService.CreateAsync(mappedStudentGroup);
        return mapper.Map<StudentGroupViewModel>(createdStudentGroup);
    }

    public async ValueTask<StudentGroupViewModel> PutAsync(long id, StudentGroupUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedStudentGroup = mapper.Map<StudentGroup>(updateModel);
        var updatedStudentGroup = await studentGroupService.UpdateAsync(id, mappedStudentGroup);
        return mapper.Map<StudentGroupViewModel>(updatedStudentGroup);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await studentGroupService.DeleteAsync(id);
    }

    public async ValueTask<StudentGroupViewModel> GetAsync(long id)
    {
        var studentGroup = await studentGroupService.GetByIdAsync(id);
        return mapper.Map<StudentGroupViewModel>(studentGroup);
    }

    public async ValueTask<IEnumerable<StudentGroupViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var studentGroups = await studentGroupService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<StudentGroupViewModel>>(studentGroups);
    }
}
