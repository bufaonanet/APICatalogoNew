using APICatalogoMinimal.Configurations;
using APICatalogoMinimal.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APICatalogoMinimal.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfig _jwtConfig;
    public TokenService(IOptions<JwtConfig> jwtConfig)
    {
        if (jwtConfig is null)
        {
            throw new ArgumentNullException(nameof(jwtConfig));
        }

        _jwtConfig = jwtConfig.Value;
    }

    public string GerarToken(UserModel user)
    {
        var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
            };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));

        var credentials = new SigningCredentials(securityKey,
                                             SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: _jwtConfig.Issuer,
                                   audience: _jwtConfig.Audience,
                                   claims: claims,
                                   expires: DateTime.Now.AddMinutes(10),
                                   signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}
