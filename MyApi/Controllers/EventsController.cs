using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.DTOs;
using MyApi.Services;

namespace MyApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventService _service;

    public EventsController(IEventService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllEventsAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ev = await _service.GetEventByIdAsync(id);
        if (ev == null) return NotFound();
        return Ok(ev);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        // For simplicity, we assume we can get the UserId from claims. 
        // In a real app, you'd have a user manager or custom base controller.
        var result = await _service.CreateEventAsync(request, Guid.Empty); 
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id}/register")]
    public async Task<IActionResult> Register(Guid id, [FromQuery] Guid employeeId)
    {
        await _service.RegisterEmployeeAsync(id, employeeId);
        return Ok();
    }

    [HttpDelete("{id}/register/{employeeId}")]
    public async Task<IActionResult> Unregister(Guid id, Guid employeeId)
    {
        await _service.UnregisterEmployeeAsync(id, employeeId);
        return Ok();
    }

    [HttpGet("{id}/participants")]
    public async Task<IActionResult> GetParticipants(Guid id)
    {
        return Ok(await _service.GetParticipantsAsync(id));
    }

    [HttpGet("{id}/gps-config")]
    public async Task<IActionResult> GetGpsConfig(Guid id)
    {
        var config = await _service.GetGpsConfigAsync(id);
        if (config == null) return NotFound();
        return Ok(config);
    }

    [HttpPost("{id}/gps-config")]
    [HttpPut("{id}/gps-config")]
    public async Task<IActionResult> SaveGpsConfig(Guid id, [FromBody] UpdateGpsConfigRequest request)
    {
        var result = await _service.SaveGpsConfigAsync(id, request);
        return Ok(result);
    }
}
