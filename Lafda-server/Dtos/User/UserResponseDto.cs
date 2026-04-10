namespace Lafda.Dtos;

using Lafda.Enums;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public UserRoleEnum Role { get; set; }
    public StatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}