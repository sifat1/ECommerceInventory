using System.IdentityModel.Tokens.Jwt;

namespace ECommerceInventory.Models.Dtos;

public class JwtDto
{
    public string token;
    public DateTime expiration;
}