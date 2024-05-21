﻿using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Configurations;
using CRM.Service.Services.Courses;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Courses;
using CRM.WebApi.Validators.Courses;

namespace CRM.WebApi.ApiService.Courses;

public class CourseApiService
    (IMapper mapper,
    ICourseService courseService,
    CourseCreateModelValidator createModelValidator,
    CourseUpdateModelValidator updateModelValidator): ICourseApiService
{
    public async ValueTask<CourseViewModel> PostAsync(CourseCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedCourse = mapper.Map<Course>(createModel);
        var createdCourse = await courseService.CreateAsync(mappedCourse);
        return mapper.Map<CourseViewModel>(createdCourse);
    }

    public async ValueTask<CourseViewModel> PutAsync(long id, CourseUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedCourse = mapper.Map<Course>(updateModelValidator);
        var updatedCourse = await courseService.UpdateAsync(id, mappedCourse);
        return mapper.Map<CourseViewModel>(mappedCourse);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await courseService.DeleteAsync(id); 
    }

    public async ValueTask<CourseViewModel> GetAsync(long id)
    {
        var course = await courseService.GetByIdAsync(id);
        return mapper.Map<CourseViewModel>(course);
    }

    public async ValueTask<IEnumerable<CourseViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var courses = await courseService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<CourseViewModel>>(courses);
    }
}
