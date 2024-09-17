using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public UserProxy Authentificate(string pseudo, string password)
        {
            var user = _usersRepository.GetUserByPseudo(pseudo, password);
            
            if (user==null)
            {
                return null;
            }
            var userProxy = new UserProxy
            {
                id = user.Id,
                firstName = user.FirstName,
                lastName = user.LastName,
                pseudo = user.pseudo,
                role = user.Role,
                mail = user.mail,
                birthDate = user.BirthDate,
                sexe = user.sexe
            };

            var tokenHandler=new JwtSecurityTokenHandler();
            //var tokenKey = Encoding.ASCII.GetBytes(key);
            byte[] tokenKey = GenerateRandomKeyWithMinimumSize(32); // Example key generation function

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                new Claim(ClaimTypes.Name, pseudo),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            userProxy.token = tokenString;
            return userProxy;
        }
        private static byte[] GenerateRandomKeyWithMinimumSize(int sizeInBytes)
        {
            // Ensure size is at least 32 bytes (256 bits)
            if (sizeInBytes < 32)
            {
                throw new ArgumentException("Key size must be at least 32 bytes (256 bits).", nameof(sizeInBytes));
            }

            // Generate random key
            byte[] key = new byte[sizeInBytes];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }
    }
}