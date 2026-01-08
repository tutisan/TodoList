using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Models;

namespace TodoList.Services;

public class AuthService
{
    public static string GenerateJWT(Account user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var privateKey = Encoding.UTF8.GetBytes(Settings.PrivateKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256
        );

        var tokenDescriptor = new SecurityTokenDescriptor{
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddHours(8),
            Subject = GenerateClaims(user),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(Account user)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
        return claims;
    }
}
