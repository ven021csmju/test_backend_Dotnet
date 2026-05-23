using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface IOrganizationService
{
    Task<IEnumerable<OrganizationResponse>> GetAllOrganizationsAsync();
    Task<OrganizationResponse?> GetOrganizationByIdAsync(Guid id);
    Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest request);

    Task<IEnumerable<DepartmentResponse>> GetDepartmentsByOrgIdAsync(Guid orgId);
    Task<DepartmentResponse> CreateDepartmentAsync(CreateDepartmentRequest request);

    Task<IEnumerable<PositionResponse>> GetAllPositionsAsync();
    Task<PositionResponse> CreatePositionAsync(CreatePositionRequest request);
}
