using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
namespace Toolkit_API.Infrastructure.Security.Jwt
{
    public class TokenGenerator
    {
        public string GenerateToken(UserSession user)
        {
            

            var key = 
            var role = user.isAdmin == 1 ? "User" : "Admin";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                
                new Claim(ClaimTypes.Name,user.Username)

            };



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
