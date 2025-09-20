using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerceInventory.Models;
using ECommerceInventory.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceInventory.Services;

public class AuthServices
{
    private readonly UserManager<UserDetails> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthServices(UserManager<UserDetails> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<bool> CreateUserAsync(UserDto userDto)
    {
        var user = new UserDetails { UserName = userDto.username, Email = userDto.email };
        var result = await _userManager.CreateAsync(user, userDto.password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create user: {errors}");
        }
        
        if (!await _roleManager.RoleExistsAsync(userDto.role??"User"))
            await _roleManager.CreateAsync(new IdentityRole(userDto.role??"User"));

        await _userManager.AddToRoleAsync(user, userDto.role??"User");
        
        return true;
    }

    public async Task<JwtDto> LoginAsync(UserDto userDto)
    {
        var user = await _userManager.FindByNameAsync(userDto.username);
        if (user != null && await _userManager.CheckPasswordAsync(user, userDto.password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new
                JwtDto {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
        
        throw new InvalidOperationException("invalid credentials"); 
    }
    
    
}