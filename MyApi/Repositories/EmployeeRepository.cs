using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task CreateAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await GetByIdAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
    {
        return await _context.Employees
            .Where(e => (e.FirstName != null && e.FirstName.Contains(keyword)) ||
                        (e.LastName != null && e.LastName.Contains(keyword)) ||
                        (e.EmployeeCode != null && e.EmployeeCode.Contains(keyword)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByOrgIdAsync(Guid orgId)
    {
        return await _context.Employees.Where(e => e.OrganizationId == orgId).ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetByDeptIdAsync(Guid deptId)
    {
        return await _context.Employees.Where(e => e.DepartmentId == deptId).ToListAsync();
    }
}
