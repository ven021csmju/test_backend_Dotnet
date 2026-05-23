using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services;

public class CheckinService : ICheckinService
{
    private readonly ICheckinRepository _repository;
    private readonly IEventRepository _eventRepository;

    public CheckinService(ICheckinRepository repository, IEventRepository eventRepository)
    {
        _repository = repository;
        _eventRepository = eventRepository;
    }

    private CheckinResponse MapToResponse(Checkin c) => new CheckinResponse
    {
        Id = c.Id,
        EventId = c.EventId,
        EmployeeId = c.EmployeeId,
        Latitude = c.Latitude,
        Longitude = c.Longitude,
        DistanceMeters = c.DistanceMeters,
        SelfiePhotoUrl = c.SelfiePhotoUrl,
        CheckedInAt = c.CheckedInAt
    };

    public async Task<CheckinResponse?> ProcessCheckinAsync(CheckinRequest request)
    {
        // 1. Check duplicate
        if (await _repository.HasAlreadyCheckedInAsync(request.EventId, request.EmployeeId))
        {
            throw new Exception("Already checked in for this event.");
        }

        // 2. Get GPS Config
        var config = await _eventRepository.GetGpsConfigAsync(request.EventId);
        if (config == null) throw new Exception("GPS configuration not found for this event.");

        // 3. Validate Time
        var now = DateTime.UtcNow;
        if (config.CheckinStart.HasValue && now < config.CheckinStart.Value)
            throw new Exception("Check-in hasn't started yet.");
        if (config.CheckinEnd.HasValue && now > config.CheckinEnd.Value)
            throw new Exception("Check-in has already ended.");

        // 4. Calculate Distance (Haversine)
        double distance = 0;
        if (config.Latitude.HasValue && config.Longitude.HasValue)
        {
            distance = CalculateDistance(
                (double)request.Latitude, (double)request.Longitude,
                (double)config.Latitude.Value, (double)config.Longitude.Value
            );

            // 5. Validate Radius
            if (config.RadiusMeters.HasValue && distance > (double)config.RadiusMeters.Value)
            {
                throw new Exception($"You are too far from the location ({distance:F2}m). Allowed radius: {config.RadiusMeters}m");
            }
        }

        // 6. Create Checkin
        var checkin = new Checkin
        {
            EventId = request.EventId,
            EmployeeId = request.EmployeeId,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            DistanceMeters = (decimal)distance,
            SelfiePhotoUrl = request.SelfiePhotoUrl
        };

        await _repository.CreateAsync(checkin);
        return MapToResponse(checkin);
    }

    public async Task<IEnumerable<CheckinResponse>> GetCheckinsByEventAsync(Guid eventId)
    {
        var results = await _repository.GetByEventIdAsync(eventId);
        return results.Select(MapToResponse);
    }

    public async Task<IEnumerable<CheckinResponse>> GetCheckinsByEmployeeAsync(Guid employeeId)
    {
        var results = await _repository.GetByEmployeeIdAsync(employeeId);
        return results.Select(MapToResponse);
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371e3; // Earth radius in meters
        var phi1 = lat1 * Math.PI / 180;
        var phi2 = lat2 * Math.PI / 180;
        var deltaPhi = (lat2 - lat1) * Math.PI / 180;
        var deltaLambda = (lon2 - lon1) * Math.PI / 180;

        var a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                Math.Cos(phi1) * Math.Cos(phi2) *
                Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return R * c;
    }
}
