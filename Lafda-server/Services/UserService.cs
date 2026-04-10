using Lafda.Dtos;
using Lafda.Entities;
using Lafda.Repositories.Interfaces;
using Lafda.Services.Interfaces;

namespace Lafda.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    // 🔐 CREATE USER (REGISTER)
    public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(CreateUserDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);

        if (existingUser != null)
            return ApiResponse<UserResponseDto>.Fail("User already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,

            // 🔥 BCrypt password hashing
            Password = HashPassword(dto.Password),

            Role = dto.Role
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };

        return ApiResponse<UserResponseDto>.Ok(response, "User created successfully");
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);

        if (user == null)
            return ApiResponse<LoginResponseDto>.Fail("User not found");

        // password verification
        if (!VerifyPassword(dto.Password, user.Password))
            return ApiResponse<LoginResponseDto>.Fail("Invalid credentials");

        // JWT token
        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

        var response = new LoginResponseDto
        {
            UserId = user.Id,
            Email = user.Email,
            Token = token
        };

        return ApiResponse<LoginResponseDto>.Ok(response, "Login successful");
    }


    // hashing
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // hash verification
    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }
}