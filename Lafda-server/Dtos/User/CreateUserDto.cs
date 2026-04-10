namespace Lafda.Dtos;

using Lafda.Enums;
public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoleEnum Role { get; set; }
}