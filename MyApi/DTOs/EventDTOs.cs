using System;
using System.Collections.Generic;

namespace MyApi.DTOs;

public class EventResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateEventRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}

public class GpsConfigResponse
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? RadiusMeters { get; set; }
    public DateTime? CheckinStart { get; set; }
    public DateTime? CheckinEnd { get; set; }
}

public class UpdateGpsConfigRequest
{
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? RadiusMeters { get; set; }
    public DateTime? CheckinStart { get; set; }
    public DateTime? CheckinEnd { get; set; }
}

public class CheckinRequest
{
    public Guid EventId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string? SelfiePhotoUrl { get; set; }
}

public class CheckinResponse
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public decimal? DistanceMeters { get; set; }
    public string? SelfiePhotoUrl { get; set; }
    public DateTime CheckedInAt { get; set; }
}
