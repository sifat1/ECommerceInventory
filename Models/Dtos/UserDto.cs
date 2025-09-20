namespace ECommerceInventory.Models.Dtos;

public class UserDto
{
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string? role{ get; set; }="User";
}