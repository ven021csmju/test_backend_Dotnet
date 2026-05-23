using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.Models;

namespace MyApi.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(Guid id);
    Task CreateAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);

    Task<IEnumerable<Employee>> SearchAsync(string keyword);
    Task<IEnumerable<Employee>> GetByOrgIdAsync(Guid orgId);
    Task<IEnumerable<Employee>> GetByDeptIdAsync(Guid deptId);
}
