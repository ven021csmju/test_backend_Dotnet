using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface IAuditLogService
{
    Task<IEnumerable<AuditLogResponse>> GetAllLogsAsync();
    Task<AuditLogResponse?> GetLogByIdAsync(Guid id);
}
