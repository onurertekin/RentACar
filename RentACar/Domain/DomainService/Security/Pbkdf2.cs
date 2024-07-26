using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace DomainService.Security
{
    public class Pbkdf2
    {
        public static Pbkdf2Result Encrypt(string plainText, string salt = "", KeyDerivationPrf keyDerivationPrf = KeyDerivationPrf.HMACSHA256)
        {
            #region GenerateRandomSalt

            byte[] _salt;

            if (salt == "")
            {
                //generate a 128-bit salt using a secure PRNG
                _salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(_salt);
                }
            }
            else
            {
                _salt = Convert.FromBase64String(salt);
            }

            #endregion

            var encryptedBytes = KeyDerivation.Pbkdf2(
                                password: plainText,
                                salt: _salt,
                                prf: keyDerivationPrf,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return new Pbkdf2Result()
            {
                EncryptedText = Convert.ToBase64String(encryptedBytes),
                Salt = Convert.ToBase64String(_salt)
            };
        }
    }

    public class Pbkdf2Result
    {
        public string EncryptedText { get; set; }
        public string Salt { get; set; }
    }
}

