using Lafda.Dtos;

namespace Lafda.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserResponseDto>> CreateUserAsync(CreateUserDto dto);

    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto);
}