using Lafda.Dtos;
using Lafda.Entities;
using Lafda.Enums;
using Lafda.Repositories.Interfaces;
using Lafda.Services.Interfaces;

namespace Lafda.Services;

public class TheoryService : ITheoryService
{
    private readonly ITheoryRepository _repo;

    public TheoryService(ITheoryRepository repo)
    {
        _repo = repo;
    }

    // ➕ CREATE THEORY
    public async Task<ApiResponse<TheoryResponseDto>> CreateAsync(CreateTheoryDto dto, int userId)
    {
        var entity = new Theory
        {
            Title = dto.Title,
            Description = dto.Description,

            PostStatus = PostStatusEnum.Waiting,

            UpVoteCount = 0,
            DownVoteCount = 0,

            UserId = userId,
            MainEventId = dto.MainEventId,
            EventId = dto.EventId
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<TheoryResponseDto>.Ok(Map(entity), "Theory created successfully");
    }

    public async Task<ApiResponse<TheoryResponseDto>> UpdateAsync(int id, UpdateTheoryDto dto, int userId)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<TheoryResponseDto>.Fail("Theory not found");

        // 🔒 RULE 1: Only owner can update
        if (entity.UserId != userId)
            return ApiResponse<TheoryResponseDto>.Fail("Unauthorized to update this theory");

        // 🔒 RULE 2: Cannot update approved theories
        if (entity.PostStatus == PostStatusEnum.Approved)
            return ApiResponse<TheoryResponseDto>.Fail("Approved theories cannot be updated");

        // ✏️ APPLY CHANGES
        entity.Title = dto.Title;
        entity.Description = dto.Description;

        if (dto.EventId.HasValue)
            entity.EventId = dto.EventId;

        _repo.Update(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<TheoryResponseDto>.Ok(Map(entity), "Theory updated successfully");
    }

    // 🔍 GET BY ID
    public async Task<ApiResponse<TheoryResponseDto>> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<TheoryResponseDto>.Fail("Theory not found");

        return ApiResponse<TheoryResponseDto>.Ok(Map(entity));
    }

    // 📋 GET ALL
    public async Task<ApiResponse<List<TheoryResponseDto>>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();

        return ApiResponse<List<TheoryResponseDto>>.Ok(
            list.Select(Map).ToList()
        );
    }



    // ❌ DELETE
    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<string>.Fail("Theory not found");

        _repo.Delete(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<string>.Ok("Deleted successfully");
    }

    // admin only
    public async Task<ApiResponse<TheoryResponseDto>> ChangeStatusAsync(int id, ChangeTheoryStatusDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<TheoryResponseDto>.Fail("Theory not found");

        // 🔥 RULE: Only status change allowed here
        entity.PostStatus = dto.PostStatus;

        _repo.Update(entity);
        await _repo.SaveChangesAsync();

        return ApiResponse<TheoryResponseDto>.Ok(Map(entity), "Status updated successfully");
    }

    // 🔄 MAPPER
    private static TheoryResponseDto Map(Theory t)
    {
        return new TheoryResponseDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,

            PostStatus = t.PostStatus,

            UpVoteCount = t.UpVoteCount,
            DownVoteCount = t.DownVoteCount,

            UserId = t.UserId,
            UserName = t.User?.Name ?? "",

            MainEventId = t.MainEventId,
            MainEventName = t.MainEvent?.Title ?? "",

            EventId = t.EventId,
            EventName = t.Event?.EventType ?? "",

            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        };
    }


}
