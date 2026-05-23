using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Repositories;

public interface IOrganizationRepository
{
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<Organization?> GetByIdAsync(Guid id);
    Task CreateAsync(Organization org);
    Task UpdateAsync(Organization org);
    Task DeleteAsync(Guid id);

    Task<IEnumerable<Department>> GetDepartmentsByOrgIdAsync(Guid orgId);
    Task CreateDepartmentAsync(Department dept);

    Task<IEnumerable<Position>> GetAllPositionsAsync();
    Task CreatePositionAsync(Position pos);
}
