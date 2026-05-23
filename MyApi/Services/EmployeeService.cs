using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    private EmployeeResponse MapToResponse(Employee e) => new EmployeeResponse
    {
        Id = e.Id,
        EmployeeCode = e.EmployeeCode,
        FirstName = e.FirstName,
        LastName = e.LastName,
        Email = e.Email,
        OrganizationId = e.OrganizationId,
        DepartmentId = e.DepartmentId,
        PositionId = e.PositionId,
        EmployeeType = e.EmployeeType,
        CreatedAt = e.CreatedAt
    };

    public async Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();
        return employees.Select(MapToResponse);
    }

    public async Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid id)
    {
        var e = await _repository.GetByIdAsync(id);
        return e == null ? null : MapToResponse(e);
    }

    public async Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request)
    {
        var e = new Employee
        {
            EmployeeCode = request.EmployeeCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            OrganizationId = request.OrganizationId,
            DepartmentId = request.DepartmentId,
            PositionId = request.PositionId,
            EmployeeType = request.EmployeeType
        };
        await _repository.CreateAsync(e);
        return MapToResponse(e);
    }

    public async Task<IEnumerable<EmployeeResponse>> SearchEmployeesAsync(string keyword)
    {
        var results = await _repository.SearchAsync(keyword);
        return results.Select(MapToResponse);
    }

    public async Task<IEnumerable<EmployeeResponse>> GetEmployeesByOrgAsync(Guid orgId)
    {
        var results = await _repository.GetByOrgIdAsync(orgId);
        return results.Select(MapToResponse);
    }

    public async Task<IEnumerable<EmployeeResponse>> GetEmployeesByDeptAsync(Guid deptId)
    {
        var results = await _repository.GetByDeptIdAsync(deptId);
        return results.Select(MapToResponse);
    }
}
