using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GMS.API.Services.JWT
{
    public class JWTManager: IJWTManager
    {
        private readonly JWTSettings _settings;

        public JWTManager(IOptions<JWTSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SigningKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: ExtractUserClaims(user),
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] ExtractUserClaims(User user)
            => new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
    }
}
