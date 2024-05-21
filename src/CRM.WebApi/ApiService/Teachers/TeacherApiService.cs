using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.Teachers;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Teachers;
using CRM.WebApi.Validators.Teachers;

namespace CRM.WebApi.ApiService.Teachers;

public class TeacherApiService
    (IMapper mapper,
    ITeacherService teacherService,
    TeacherCreateModelValidator createModelValidator,
    TeacherUpdateModelValidator updateModelValidator): ITeacherApiService
{
    public async ValueTask<TeacherViewModel> PostAsync(TeacherCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedTeacher = mapper.Map<Teacher>(createModel);
        var createdTeacher = await teacherService.CreateAsync(mappedTeacher);
        return mapper.Map<TeacherViewModel>(createdTeacher);
    }

    public async ValueTask<TeacherViewModel> PutAsync(long id, TeacherUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedTeacher = mapper.Map<Teacher>(updateModel);
        var updatedTeacher = await teacherService.UpdateAsync(id, mappedTeacher);
        return mapper.Map<TeacherViewModel>(updatedTeacher);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await teacherService.DeleteAsync(id);
    }

    public async ValueTask<TeacherViewModel> GetAsync(long id)
    {
        var teacher = await teacherService.GetByIdAsync(id);
        return mapper.Map<TeacherViewModel>(teacher);
    }

    public async ValueTask<IEnumerable<TeacherViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var teachers = await teacherService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<TeacherViewModel>>(teachers);
    }
}
