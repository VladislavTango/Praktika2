using Microsoft.IdentityModel.Tokens;
using PraktikaDomain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PraktikaInfrastructure.JWT
{
    public class JwtTokenService : IJwtTokentService
    {
        private readonly string secretKey = "VQF3Dv69xcmTZqK4g6gepHZYSLSHT9G2";
        private readonly string issuer = "keklik";
        private readonly string audience = "Yabloki";
        private readonly TimeSpan expiration = TimeSpan.FromDays(30);

        public string GenerateToken(int userId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Email, userEmail),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            var token = new JwtSecurityToken
                (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (int userId, string userEmail) TokenClaimCatcher(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (emailClaim == null || userIdClaim == null)
                throw new Exception("Не удалось извлечь клеймы из токена.");

            if (!int.TryParse(userIdClaim, out int userId))
                throw new Exception("Неверный формат userId в токене.");

            return (userId, emailClaim);
        }
    }
}
