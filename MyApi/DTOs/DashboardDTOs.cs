using System;
using System.Collections.Generic;

namespace MyApi.DTOs;

public class DashboardSummaryResponse
{
    public int TotalEvents { get; set; }
    public int TotalEmployees { get; set; }
    public int TotalCheckins { get; set; }
    public double AttendanceRate { get; set; }
}

public class AuditLogResponse
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Action { get; set; }
    public string? TableName { get; set; }
    public Guid? RecordId { get; set; }
    public string? OldData { get; set; }
    public string? NewData { get; set; }
    public DateTime CreatedAt { get; set; }
}
