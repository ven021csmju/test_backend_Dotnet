using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories;

namespace MyApi.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _repository;

    public OrganizationService(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrganizationResponse>> GetAllOrganizationsAsync()
    {
        var orgs = await _repository.GetAllAsync();
        return orgs.Select(o => new OrganizationResponse
        {
            Id = o.Id,
            Code = o.Code,
            Name = o.Name,
            Type = o.Type,
            ParentId = o.ParentId
        });
    }

    public async Task<OrganizationResponse?> GetOrganizationByIdAsync(Guid id)
    {
        var o = await _repository.GetByIdAsync(id);
        if (o == null) return null;
        return new OrganizationResponse
        {
            Id = o.Id,
            Code = o.Code,
            Name = o.Name,
            Type = o.Type,
            ParentId = o.ParentId
        };
    }

    public async Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        var org = new Organization
        {
            Code = request.Code,
            Name = request.Name,
            Type = request.Type,
            ParentId = request.ParentId
        };
        await _repository.CreateAsync(org);
        return new OrganizationResponse
        {
            Id = org.Id,
            Code = org.Code,
            Name = org.Name,
            Type = org.Type,
            ParentId = org.ParentId
        };
    }

    public async Task<IEnumerable<DepartmentResponse>> GetDepartmentsByOrgIdAsync(Guid orgId)
    {
        var depts = await _repository.GetDepartmentsByOrgIdAsync(orgId);
        return depts.Select(d => new DepartmentResponse
        {
            Id = d.Id,
            OrganizationId = d.OrganizationId,
            Name = d.Name
        });
    }

    public async Task<DepartmentResponse> CreateDepartmentAsync(CreateDepartmentRequest request)
    {
        var dept = new Department
        {
            OrganizationId = request.OrganizationId,
            Name = request.Name
        };
        await _repository.CreateDepartmentAsync(dept);
        return new DepartmentResponse
        {
            Id = dept.Id,
            OrganizationId = dept.OrganizationId,
            Name = dept.Name
        };
    }

    public async Task<IEnumerable<PositionResponse>> GetAllPositionsAsync()
    {
        var positions = await _repository.GetAllPositionsAsync();
        return positions.Select(p => new PositionResponse
        {
            Id = p.Id,
            Name = p.Name
        });
    }

    public async Task<PositionResponse> CreatePositionAsync(CreatePositionRequest request)
    {
        var pos = new Position
        {
            Name = request.Name
        };
        await _repository.CreatePositionAsync(pos);
        return new PositionResponse
        {
            Id = pos.Id,
            Name = pos.Name
        };
    }
}
