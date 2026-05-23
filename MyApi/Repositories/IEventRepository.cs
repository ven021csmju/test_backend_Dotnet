using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<SafetyEvent>> GetAllAsync();
    Task<SafetyEvent?> GetByIdAsync(Guid id);
    Task CreateAsync(SafetyEvent ev);
    Task UpdateAsync(SafetyEvent ev);
    Task DeleteAsync(Guid id);

    Task RegisterEmployeeAsync(EventRegistration registration);
    Task UnregisterEmployeeAsync(Guid eventId, Guid employeeId);
    Task<IEnumerable<Employee>> GetParticipantsAsync(Guid eventId);

    Task<GpsConfig?> GetGpsConfigAsync(Guid eventId);
    Task SaveGpsConfigAsync(GpsConfig config);
}
