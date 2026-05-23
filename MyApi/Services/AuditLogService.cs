using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    private AuditLogResponse MapToResponse(AuditLog l) => new AuditLogResponse
    {
        Id = l.Id,
        UserId = l.UserId,
        Action = l.Action,
        TableName = l.TableName,
        RecordId = l.RecordId,
        OldData = l.OldData,
        NewData = l.NewData,
        CreatedAt = l.CreatedAt
    };

    public async Task<IEnumerable<AuditLogResponse>> GetAllLogsAsync()
    {
        var logs = await _repository.GetAllAsync();
        return logs.Select(MapToResponse);
    }

    public async Task<AuditLogResponse?> GetLogByIdAsync(Guid id)
    {
        var log = await _repository.GetByIdAsync(id);
        return log == null ? null : MapToResponse(log);
    }
}
