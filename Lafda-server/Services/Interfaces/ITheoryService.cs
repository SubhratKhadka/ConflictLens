using Lafda.Dtos;

namespace Lafda.Services.Interfaces;

public interface ITheoryService
{
    Task<ApiResponse<TheoryResponseDto>> CreateAsync(CreateTheoryDto dto, int userId);
    Task<ApiResponse<TheoryResponseDto>> UpdateAsync(int id, UpdateTheoryDto dto, int userId);

    Task<ApiResponse<TheoryResponseDto>> GetByIdAsync(int id);

    Task<ApiResponse<List<TheoryResponseDto>>> GetAllAsync();

    Task<ApiResponse<string>> DeleteAsync(int id);

    // admin only
    Task<ApiResponse<TheoryResponseDto>> ChangeStatusAsync(int id, ChangeTheoryStatusDto dto);
}