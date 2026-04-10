using Lafda.Dtos;
using Lafda.Entities;
using Lafda.Repositories.Interfaces;
using Lafda.Services.Interfaces;

namespace Lafda.Services;

public class MainEventService : IMainEventService
{
    private readonly IMainEventRepository _repo;

    public MainEventService(IMainEventRepository repo)
    {
        _repo = repo;
    }

    // ➕ CREATE
    public async Task<ApiResponse<MainEventResponseDto>> CreateAsync(CreateMainEventDto dto)
    {
        var entity = new MainEvent
        {
            Title = dto.Title,
            Description = dto.Description,

            // 🔥 default already handled in entity, but explicit is fine
            StartDate = dto.StartDate == default
                ? DateOnly.FromDateTime(DateTime.UtcNow)
                : dto.StartDate
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<MainEventResponseDto>.Ok(Map(entity), "MainEvent created");
    }

    // 🔍 GET BY ID
    public async Task<ApiResponse<MainEventResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<MainEventResponseDto>.Fail("MainEvent not found");

        return ApiResponse<MainEventResponseDto>.Ok(Map(entity));
    }

    // 📋 GET ALL
    public async Task<ApiResponse<List<MainEventResponseDto>>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();

        return ApiResponse<List<MainEventResponseDto>>.Ok(
            list.Select(Map).ToList()
        );
    }

    // ❌ DELETE
    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<string>.Fail("MainEvent not found");

        _repo.Delete(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<string>.Ok("Deleted successfully");
    }

    // 🔄 MAPPER
    private static MainEventResponseDto Map(MainEvent e)
    {
        return new MainEventResponseDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate
        };
    }
}