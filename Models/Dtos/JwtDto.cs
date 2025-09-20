using System.IdentityModel.Tokens.Jwt;

namespace ECommerceInventory.Models.Dtos;

public class JwtDto
{
    public string token { get; set; }
    public string refreshToken { get; set; }   // Add this
    public DateTime expiration { get; set; }
}