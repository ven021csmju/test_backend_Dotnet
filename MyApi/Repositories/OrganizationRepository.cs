using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _context.Organizations.ToListAsync();
    }

    public async Task<Organization?> GetByIdAsync(Guid id)
    {
        return await _context.Organizations.FindAsync(id);
    }

    public async Task CreateAsync(Organization org)
    {
        await _context.Organizations.AddAsync(org);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Organization org)
    {
        _context.Organizations.Update(org);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var org = await GetByIdAsync(id);
        if (org != null)
        {
            _context.Organizations.Remove(org);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Department>> GetDepartmentsByOrgIdAsync(Guid orgId)
    {
        return await _context.Departments.Where(d => d.OrganizationId == orgId).ToListAsync();
    }

    public async Task CreateDepartmentAsync(Department dept)
    {
        await _context.Departments.AddAsync(dept);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Position>> GetAllPositionsAsync()
    {
        return await _context.Positions.ToListAsync();
    }

    public async Task CreatePositionAsync(Position pos)
    {
        await _context.Positions.AddAsync(pos);
        await _context.SaveChangesAsync();
    }
}
