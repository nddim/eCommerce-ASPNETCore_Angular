using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Crypto.Generators;

namespace WebAPI.Helpers.Auth.PasswordHasher
{
    public class PasswordHasher:IPasswordHasher
    {
        //private const int SaltSize = 32;
        //private const int KeySize = 32;
        //private const int Iterations = 25555;
        //private static readonly HashAlgorithmName _hashAlgorithmName=HashAlgorithmName.SHA256;
        //private static char Delimiter = ';';

        public async Task<string> Hash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return passwordHash;
            //var salt = RandomNumberGenerator.GetBytes(SaltSize);
            //var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);

            //return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public async Task<bool> Verify(string passwordHash, string inputPassword)
        {
            var result = BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, passwordHash);
            return result;
            //var elements = passwordHash.Split(Delimiter);
            //var salt = Convert.FromBase64String(elements[0]);
            //var hash = Convert.FromBase64String(elements[1]);

            //var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);

            //return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
