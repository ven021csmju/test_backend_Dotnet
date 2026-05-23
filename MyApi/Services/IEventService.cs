using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface IEventService
{
    Task<IEnumerable<EventResponse>> GetAllEventsAsync();
    Task<EventResponse?> GetEventByIdAsync(Guid id);
    Task<EventResponse> CreateEventAsync(CreateEventRequest request, Guid userId);

    Task RegisterEmployeeAsync(Guid eventId, Guid employeeId);
    Task UnregisterEmployeeAsync(Guid eventId, Guid employeeId);
    Task<IEnumerable<EmployeeResponse>> GetParticipantsAsync(Guid eventId);

    Task<GpsConfigResponse?> GetGpsConfigAsync(Guid eventId);
    Task<GpsConfigResponse> SaveGpsConfigAsync(Guid eventId, UpdateGpsConfigRequest request);
}
