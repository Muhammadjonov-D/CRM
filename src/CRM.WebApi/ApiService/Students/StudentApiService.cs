using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.Students;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.StudentGroups;
using CRM.WebApi.Models.Students;
using CRM.WebApi.Validators.Students;

namespace CRM.WebApi.ApiService.Students;

public class StudentApiService
    (IMapper mapper,
    IStudentService studentService,
    StudentCreateModelValidator createModelValidator,
    StudentUpdateModelValidator updateModelValidator): IStudentApiService
{
    public async ValueTask<StudentViewModel> PostAsync(StudentCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedStudent = mapper.Map<Student>(createModel);
        var createdStudent = await studentService.CreateAsync(mappedStudent);
        return mapper.Map<StudentViewModel>(createdStudent);
    }

    public async ValueTask<StudentViewModel> PutAsync(long id, StudentUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedStudent = mapper.Map<Student>(updateModel);
        var updatedStudent = await studentService.UpdateAsync(id, mappedStudent);
        return mapper.Map<StudentViewModel>(updatedStudent);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await studentService.DeleteAsync(id);
    }

    public async ValueTask<StudentViewModel> GetAsync(long id)
    {
        var student = await studentService.GetByIdAsync(id);
        return mapper.Map<StudentViewModel>(student);
    }

    public async ValueTask<IEnumerable<StudentViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var students = await studentService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<StudentViewModel>>(students);
    }
}
