namespace ECommerceInventory.Models.Dtos;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Address { get; set; }
    public string? Role { get; set; } = "User";
}