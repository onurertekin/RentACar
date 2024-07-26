using DatabaseModel;
using DatabaseModel.Entities;
using DomainService.Exceptions;
using DomainService.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomainService.Operations
{
    public class AuthenticationOperations
    {
        private readonly MainDbContext mainDbContext;
        public AuthenticationOperations(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public string Authentication(string userName, string password)
        {
            #region Validations

            var user = mainDbContext.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user == null)
                throw new BusinessException(404, "Kullanıcı adı veya şifre geçersiz");

            var pbdfk2Result = Pbkdf2.Encrypt(password, user.PasswordSalt);
            if (pbdfk2Result.EncryptedText != user.Password)
                throw new BusinessException(401, "Kullanıcı adı veya şifre geçersiz");

            #endregion

            var token = GenerateToken(user.Id, password);
            return token;
        }

        public string GenerateToken(int userId, string userName)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyBurayaGelecekSecretKeyBurayaGelecekSecretKeyBurayaGelecek"));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddDays(2);

            var claims = new List<Claim>();
            claims.Add(new Claim("UserId", userId.ToString()));
            claims.Add(new Claim("Email", userName));

            var jwtSecurityToken = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

    }
}
