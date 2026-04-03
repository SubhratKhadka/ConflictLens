namespace Lafda.Entities;

using Lafda.Enums;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoleEnum Role { get; set; }
    // currently we shall store img in db
    public byte[] ProfileImage { get; set; }
}