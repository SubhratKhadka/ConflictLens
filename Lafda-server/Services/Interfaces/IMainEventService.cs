using Lafda.Dtos;

namespace Lafda.Services.Interfaces;

public interface IMainEventService
{
    Task<ApiResponse<MainEventResponseDto>> CreateAsync(CreateMainEventDto dto);

    Task<ApiResponse<MainEventResponseDto>> GetByIdAsync(int id);

    Task<ApiResponse<List<MainEventResponseDto>>> GetAllAsync();

    Task<ApiResponse<string>> DeleteAsync(int id);
}