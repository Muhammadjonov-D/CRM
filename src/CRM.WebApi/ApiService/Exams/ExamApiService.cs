using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.Exams;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Exams;
using CRM.WebApi.Validators.Exams;

namespace CRM.WebApi.ApiService.Exams;

public class ExamApiService
    (IMapper mapper,
    IExamService examService,
    ExamCreateModelValidator createModelValidator,
    ExamUpdateModelValidator updateModelValidator) : IExamApiService
{
    public async ValueTask<ExamViewModel> PostAsync(ExamCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedExam = mapper.Map<Exam>(createModel);
        var createdExam = await examService.CreateAsync(mappedExam);
        return mapper.Map<ExamViewModel>(createdExam);

    }

    public async ValueTask<ExamViewModel> PutAsync(long id, ExamUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedExam = mapper.Map<Exam>(updateModel);
        var updatedExam = await examService.UpdateAsync(id, mappedExam);
        return mapper.Map<ExamViewModel>(updatedExam);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await examService.DeleteAsync(id);
    }

    public async ValueTask<ExamViewModel> GetAsync(long id)
    {
        var exam = await examService.GetByIdAsync(id);
        return mapper.Map<ExamViewModel>(exam);
    }

    public async ValueTask<IEnumerable<ExamViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var exams = await examService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<ExamViewModel>>(exams);
    }
}
