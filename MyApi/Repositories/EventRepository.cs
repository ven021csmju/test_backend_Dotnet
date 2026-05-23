using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SafetyEvent>> GetAllAsync()
    {
        return await _context.SafetyEvents.ToListAsync();
    }

    public async Task<SafetyEvent?> GetByIdAsync(Guid id)
    {
        return await _context.SafetyEvents.FindAsync(id);
    }

    public async Task CreateAsync(SafetyEvent ev)
    {
        await _context.SafetyEvents.AddAsync(ev);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SafetyEvent ev)
    {
        _context.SafetyEvents.Update(ev);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ev = await GetByIdAsync(id);
        if (ev != null)
        {
            _context.SafetyEvents.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }

    public async Task RegisterEmployeeAsync(EventRegistration registration)
    {
        await _context.EventRegistrations.AddAsync(registration);
        await _context.SaveChangesAsync();
    }

    public async Task UnregisterEmployeeAsync(Guid eventId, Guid employeeId)
    {
        var reg = await _context.EventRegistrations.FirstOrDefaultAsync(r => r.EventId == eventId && r.EmployeeId == employeeId);
        if (reg != null)
        {
            _context.EventRegistrations.Remove(reg);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Employee>> GetParticipantsAsync(Guid eventId)
    {
        return await _context.EventRegistrations
            .Where(r => r.EventId == eventId)
            .Select(r => r.Employee!)
            .ToListAsync();
    }

    public async Task<GpsConfig?> GetGpsConfigAsync(Guid eventId)
    {
        return await _context.GpsConfigs.FirstOrDefaultAsync(g => g.EventId == eventId);
    }

    public async Task SaveGpsConfigAsync(GpsConfig config)
    {
        var existing = await GetGpsConfigAsync(config.EventId);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(config);
        }
        else
        {
            await _context.GpsConfigs.AddAsync(config);
        }
        await _context.SaveChangesAsync();
    }
}
