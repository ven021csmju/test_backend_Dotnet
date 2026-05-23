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
public class CheckinsController : ControllerBase
{
    private readonly ICheckinService _service;

    public CheckinsController(ICheckinService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CheckinRequest request)
    {
        try
        {
            var result = await _service.ProcessCheckinAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetByEvent(Guid eventId)
    {
        return Ok(await _service.GetCheckinsByEventAsync(eventId));
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(Guid employeeId)
    {
        return Ok(await _service.GetCheckinsByEmployeeAsync(employeeId));
    }
}
