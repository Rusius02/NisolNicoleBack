using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;



namespace Infrastructure.SqlServer.Repository.Users
{
    public class JwtAuthentificationManager: IJwtAuthentificationManager
    {
        private readonly UsersRepository _usersRepository=new UsersRepository();
        private readonly string key;
        public IConfiguration Configuration { get; }

        public JwtAuthentificationManager(string key, IConfiguration configuration)
        {
            this.key = key;
            Configuration = configuration;
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
            var secretKey = Configuration["JwtSettings:SecretKey"];
            var tokenKey = Encoding.ASCII.GetBytes(secretKey);

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
    }
}