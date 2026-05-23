using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface ICheckinService
{
    Task<CheckinResponse?> ProcessCheckinAsync(CheckinRequest request);
    Task<IEnumerable<CheckinResponse>> GetCheckinsByEventAsync(Guid eventId);
    Task<IEnumerable<CheckinResponse>> GetCheckinsByEmployeeAsync(Guid employeeId);
}
