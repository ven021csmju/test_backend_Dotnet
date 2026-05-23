using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApi.DTOs;

namespace MyApi.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
    Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid id);
    Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest request);

    Task<IEnumerable<EmployeeResponse>> SearchEmployeesAsync(string keyword);
    Task<IEnumerable<EmployeeResponse>> GetEmployeesByOrgAsync(Guid orgId);
    Task<IEnumerable<EmployeeResponse>> GetEmployeesByDeptAsync(Guid deptId);
}
