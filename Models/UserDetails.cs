using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ECommerceInventory.Models;

public class UserDetails : IdentityUser
{
    [StringLength(100)]
    public string FirstName { get; set; }
    [StringLength(100)]
    public string LastName { get; set; }
    [StringLength(200)]
    public string Address { get; set; }

    public string? RefreshToken { get; set; } = "";
    public DateTime? RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow.AddDays(7);
}