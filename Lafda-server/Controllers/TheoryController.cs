using Lafda.Dtos;
using Lafda.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lafda.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TheoryController : ControllerBase
{
    private readonly ITheoryService _theoryService;

    public TheoryController(ITheoryService theoryService)
    {
        _theoryService = theoryService;
    }

    // ➕ CREATE THEORY
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTheoryDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized("User not found in token");

        int userId = int.Parse(userIdClaim);

        var result = await _theoryService.CreateAsync(dto, userId);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTheoryDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        var result = await _theoryService.UpdateAsync(id, dto, userId);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _theoryService.GetByIdAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    // GET ALL
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _theoryService.GetAllAsync();
        return Ok(result);
    }

    // DELETE THEORY
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _theoryService.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeTheoryStatusDto dto)
    {
        var result = await _theoryService.ChangeStatusAsync(id, dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }
}