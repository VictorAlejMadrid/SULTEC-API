using Microsoft.IdentityModel.Tokens;
using SULTEC_API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SULTEC_API.Services;

public class TokenService
{
    private readonly IConfiguration configuration;

    public TokenService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        Claim[] claims =
        {
            new Claim("Id", user.Id),
            new Claim("username", user.UserName!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VDN;0]DjErm<!e9k*}141)2Ez4Ah))senhasenha"));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddHours(12),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
