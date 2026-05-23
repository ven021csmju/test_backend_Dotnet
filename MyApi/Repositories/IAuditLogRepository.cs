using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Repositories;

public interface IAuditLogRepository
{
    Task<IEnumerable<AuditLog>> GetAllAsync();
    Task<AuditLog?> GetByIdAsync(Guid id);
    Task CreateAsync(AuditLog log);
}
