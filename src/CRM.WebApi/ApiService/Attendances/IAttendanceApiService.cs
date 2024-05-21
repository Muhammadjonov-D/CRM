using CRM.Service.Configurations;
using CRM.WebApi.Models.Attendances;

namespace CRM.WebApi.ApiService.Attendances;

public interface IAttendanceApiService
{
    ValueTask<AttendanceViewModel> PostAsync(AttendanceCreateModel createModel);
    ValueTask<AttendanceViewModel> PutAsync(long id, AttendanceUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
}
