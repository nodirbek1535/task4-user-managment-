//==============================================================
//Nasrullayev Nodirbek's UserManagment project
//==============================================================

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using task4_user_managment_.Brokers.Security;
using UserManagement.Core.Models.Users;

namespace task4_user_managment_.Services.Foundations.Security
{
    public class TokenService : ITokenService
    {
        private readonly IRandomBroker randomBroker;
        private readonly IConfiguration configuration;

        public TokenService(IRandomBroker randomBroker, IConfiguration configuration)
        {
            this.randomBroker = randomBroker;
            this.configuration = configuration;
        }

        public string GenerateEmailConfirmationToken() =>
            this.randomBroker.GenerateString(32);

        public DateTime GetTokenExpirationTime() =>
            DateTime.UtcNow.AddHours(1);

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationInMinutes"]!)),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
