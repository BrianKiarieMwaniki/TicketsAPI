using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketsAPI.Auth
{
    public class JwtTokenManager : ICustomTokenManager
    {
        private JwtSecurityTokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;
        private byte[] secretKey;

        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSecretKey"));
        }
        public string CreateToken(string username)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));

            if(username.Equals("brian", StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token =_tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }

        public string GetUserInfoByToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var jwtToken = _tokenHandler.ReadToken(token.Replace("\"",string.Empty)) as JwtSecurityToken;
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name");

            if (claim == null) return null;

            return claim.Value.ToString();
        }

        public bool VerifyToken(string token)
        {
           if(string.IsNullOrEmpty(token)) return false;

            SecurityToken securityToken;

            try
            {
                _tokenHandler.ValidateToken(token.Replace("\"", string.Empty),
                       new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                           ValidateLifetime = true,
                           ValidateAudience = false,
                           ValidateIssuer = false,
                           ClockSkew = TimeSpan.Zero
                       },
                       out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }
            catch(Exception)
            {
                throw;
            }


            return securityToken != null;
        }
    }
}
