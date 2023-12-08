using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Helpers
{
    public class JWT
    {

        public static string generateToken(List<Claim> claims, IConfiguration config)
        {

            // If registration is successful, proceed to generate token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var jwtIssuer = config.GetSection("Jwt:Issuer").Get<string>();

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials,
                claims: claims
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;

        }
    }
}
