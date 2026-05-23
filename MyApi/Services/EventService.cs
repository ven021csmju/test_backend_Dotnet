using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    private EventResponse MapToResponse(SafetyEvent e) => new EventResponse
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        StartTime = e.StartTime,
        EndTime = e.EndTime,
        CreatedBy = e.CreatedBy,
        CreatedAt = e.CreatedAt
    };

    public async Task<IEnumerable<EventResponse>> GetAllEventsAsync()
    {
        var events = await _repository.GetAllAsync();
        return events.Select(MapToResponse);
    }

    public async Task<EventResponse?> GetEventByIdAsync(Guid id)
    {
        var e = await _repository.GetByIdAsync(id);
        return e == null ? null : MapToResponse(e);
    }

    public async Task<EventResponse> CreateEventAsync(CreateEventRequest request, Guid userId)
    {
        var e = new SafetyEvent
        {
            Title = request.Title,
            Description = request.Description,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            CreatedBy = userId
        };
        await _repository.CreateAsync(e);
        return MapToResponse(e);
    }

    public async Task RegisterEmployeeAsync(Guid eventId, Guid employeeId)
    {
        await _repository.RegisterEmployeeAsync(new EventRegistration
        {
            EventId = eventId,
            EmployeeId = employeeId
        });
    }

    public async Task UnregisterEmployeeAsync(Guid eventId, Guid employeeId)
    {
        await _repository.UnregisterEmployeeAsync(eventId, employeeId);
    }

    public async Task<IEnumerable<EmployeeResponse>> GetParticipantsAsync(Guid eventId)
    {
        var employees = await _repository.GetParticipantsAsync(eventId);
        return employees.Select(e => new EmployeeResponse
        {
            Id = e.Id,
            EmployeeCode = e.EmployeeCode,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email
        });
    }

    public async Task<GpsConfigResponse?> GetGpsConfigAsync(Guid eventId)
    {
        var g = await _repository.GetGpsConfigAsync(eventId);
        if (g == null) return null;
        return new GpsConfigResponse
        {
            Id = g.Id,
            EventId = g.EventId,
            Latitude = g.Latitude,
            Longitude = g.Longitude,
            RadiusMeters = g.RadiusMeters,
            CheckinStart = g.CheckinStart,
            CheckinEnd = g.CheckinEnd
        };
    }

    public async Task<GpsConfigResponse> SaveGpsConfigAsync(Guid eventId, UpdateGpsConfigRequest request)
    {
        var g = new GpsConfig
        {
            EventId = eventId,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            RadiusMeters = request.RadiusMeters,
            CheckinStart = request.CheckinStart,
            CheckinEnd = request.CheckinEnd
        };
        await _repository.SaveGpsConfigAsync(g);
        return new GpsConfigResponse
        {
            Id = g.Id,
            EventId = g.EventId,
            Latitude = g.Latitude,
            Longitude = g.Longitude,
            RadiusMeters = g.RadiusMeters,
            CheckinStart = g.CheckinStart,
            CheckinEnd = g.CheckinEnd
        };
    }
}
