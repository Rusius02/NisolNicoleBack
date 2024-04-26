using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.SqlServer.Repository.Users
{
    public class JwtAuthentificationManager: IJwtAuthentificationManager
    {
        private readonly UsersRepository _usersRepository=new UsersRepository();
        private readonly string key;

        public JwtAuthentificationManager(string key)
        {
            this.key = key;
        }
        public string Authentificate(string pseudo, string password)
        {
            var user = _usersRepository.GetUserByPseudo(pseudo, password);
            if (user==null)
            {
                return null;
            }

            
            var tokenHandler=new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,pseudo),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}