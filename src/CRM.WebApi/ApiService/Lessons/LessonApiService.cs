using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.Lessons;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Lessons;
using CRM.WebApi.Validators.Lessons;

namespace CRM.WebApi.ApiService.Lessons;

public class LessonApiService
    (IMapper mapper,
    ILessonService lessonService,
    LessonCreateModelValidator createModelValidator,
    LessonUpdateModelValidator updateModelValidator) : ILessonApiService
{
    public async ValueTask<LessonViewModel> PostAsync(LessonCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedLesson = mapper.Map<Lesson>(createModel);
        var createdLesson = lessonService.CreateAsync(mappedLesson);
        return mapper.Map<LessonViewModel>(createdLesson);
    }

    public async ValueTask<LessonViewModel> PutAsync(long id, LessonUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedLesson = mapper.Map<Lesson>(updateModel);
        var updatedLesson = lessonService.UpdateAsync(id, mappedLesson);
        return mapper.Map<LessonViewModel>(updatedLesson);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await lessonService.DeleteAsync(id);
    }

    public async ValueTask<LessonViewModel> GetAsync(long id)
    {
        var lesson = await lessonService.GetByIdAsync(id);
        return mapper.Map<LessonViewModel>(lesson);
    }

    public async ValueTask<IEnumerable<LessonViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var lessons = await lessonService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<LessonViewModel>>(lessons);
    }
}
