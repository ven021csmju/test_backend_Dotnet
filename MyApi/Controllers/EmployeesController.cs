using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;

namespace MyApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllEmployeesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var employee = await _service.GetEmployeeByIdAsync(id);
        if (employee == null) return NotFound();
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        var result = await _service.CreateEmployeeAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        return Ok(await _service.SearchEmployeesAsync(keyword));
    }

    [HttpGet("by-organization/{orgId}")]
    public async Task<IActionResult> GetByOrg(Guid orgId)
    {
        return Ok(await _service.GetEmployeesByOrgAsync(orgId));
    }

    [HttpGet("by-department/{deptId}")]
    public async Task<IActionResult> GetByDept(Guid deptId)
    {
        return Ok(await _service.GetEmployeesByDeptAsync(deptId));
    }
}
