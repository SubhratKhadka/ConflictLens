using Lafda.Dtos;

namespace Lafda.Services.Interfaces;

public interface IEventService
{
    Task<ApiResponse<EventResponseDto>> CreateEventAsync(CreateEventDto dto, int userId);
    Task<ApiResponse<EventResponseDto>> GetByIdAsync(int id);
    Task<ApiResponse<List<EventResponseDto>>> GetAllAsync();
    Task<ApiResponse<string>> DeleteAsync(int id);
}