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
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _service;

    public OrganizationsController(IOrganizationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllOrganizationsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var org = await _service.GetOrganizationByIdAsync(id);
        if (org == null) return NotFound();
        return Ok(org);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
    {
        var result = await _service.CreateOrganizationAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}/departments")]
    public async Task<IActionResult> GetDepartments(Guid id)
    {
        return Ok(await _service.GetDepartmentsByOrgIdAsync(id));
    }

    [HttpPost("departments")]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        var result = await _service.CreateDepartmentAsync(request);
        return Ok(result);
    }

    [HttpGet("positions")]
    public async Task<IActionResult> GetPositions()
    {
        return Ok(await _service.GetAllPositionsAsync());
    }

    [HttpPost("positions")]
    public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest request)
    {
        var result = await _service.CreatePositionAsync(request);
        return Ok(result);
    }
}
