using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace User.Application.PasswordHash
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, out string salt)
        {
            int iterations = 10000;
            int keySize = 64;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] saltBytes = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, iterations, hashAlgorithm, keySize);
            salt = Convert.ToHexString(saltBytes);
            return Convert.ToHexString(hash);
        }
        public static bool VerifyPassword(string password, string salt, string passwordHash)
        {
            int iterations = 10000;
            int keySize = 64;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] saltBytes = Convert.FromHexString(salt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, iterations, hashAlgorithm, keySize);
            return Convert.ToHexString(hash) == passwordHash;
        }
    }
}