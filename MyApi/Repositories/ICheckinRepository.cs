using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Repositories;

public interface ICheckinRepository
{
    Task<IEnumerable<Checkin>> GetAllAsync();
    Task<Checkin?> GetByIdAsync(Guid id);
    Task CreateAsync(Checkin checkin);

    Task<IEnumerable<Checkin>> GetByEventIdAsync(Guid eventId);
    Task<IEnumerable<Checkin>> GetByEmployeeIdAsync(Guid employeeId);
    Task<bool> HasAlreadyCheckedInAsync(Guid eventId, Guid employeeId);
}
