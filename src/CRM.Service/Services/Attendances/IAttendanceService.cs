using CRM.Domain.Entities;

namespace CRM.Service.Services.Attendances;

public interface IAttendanceService
{
    ValueTask<Attendance> CreateAsync(Attendance attendance);
    ValueTask<Attendance> UpdateAsync(long id, Attendance attendance);
    ValueTask<bool> DeleteAsync(long id);
}
