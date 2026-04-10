using Lafda.Dtos;
using Lafda.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lafda.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // 🔐 require login for all endpoints (optional but recommended)
public class MainEventController : ControllerBase
{
    private readonly IMainEventService _mainEventService;

    public MainEventController(IMainEventService mainEventService)
    {
        _mainEventService = mainEventService;
    }

    // ➕ CREATE MAIN EVENT
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMainEventDto dto)
    {
        var result = await _mainEventService.CreateAsync(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    // 🔍 GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mainEventService.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    // 📋 GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mainEventService.GetAllAsync();
        return Ok(result);
    }

    // ❌ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mainEventService.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }
}