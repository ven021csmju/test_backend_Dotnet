using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Repositories;

public class CheckinRepository : ICheckinRepository
{
    private readonly AppDbContext _context;

    public CheckinRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Checkin>> GetAllAsync()
    {
        return await _context.Checkins.ToListAsync();
    }

    public async Task<Checkin?> GetByIdAsync(Guid id)
    {
        return await _context.Checkins.FindAsync(id);
    }

    public async Task CreateAsync(Checkin checkin)
    {
        await _context.Checkins.AddAsync(checkin);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Checkin>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.Checkins.Where(c => c.EventId == eventId).ToListAsync();
    }

    public async Task<IEnumerable<Checkin>> GetByEmployeeIdAsync(Guid employeeId)
    {
        return await _context.Checkins.Where(c => c.EmployeeId == employeeId).ToListAsync();
    }

    public async Task<bool> HasAlreadyCheckedInAsync(Guid eventId, Guid employeeId)
    {
        return await _context.Checkins.AnyAsync(c => c.EventId == eventId && c.EmployeeId == employeeId);
    }
}
