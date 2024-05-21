using AutoMapper;
using CRM.Domain.Entities;
using CRM.Service.Services.Attendances;
using CRM.WebApi.Extensions;
using CRM.WebApi.Models.Attendances;
using CRM.WebApi.Validators.Attendances;

namespace CRM.WebApi.ApiService.Attendances;

public class AttendanceApiService
    (IMapper mapper,
    IAttendanceService attendanceService,
    AttendanceCreateModelValidator createModelValidator,
    AttendanceUpdateModelValidator updateModelValidator) : IAttendanceApiService
{
    public async ValueTask<AttendanceViewModel> PostAsync(AttendanceCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedAttendance = mapper.Map<Attendance>(createModel);
        var createdAttendance = await attendanceService.CreateAsync(mappedAttendance);
        return mapper.Map<AttendanceViewModel>(createdAttendance);
    }

    public async ValueTask<AttendanceViewModel> PutAsync(long id, AttendanceUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedAttendance = mapper.Map<Attendance>(updateModel);
        var updatedAttendance = await attendanceService.UpdateAsync(id, mappedAttendance);
        return mapper.Map<AttendanceViewModel>(updatedAttendance);

    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await attendanceService.DeleteAsync(id);
    }
}
