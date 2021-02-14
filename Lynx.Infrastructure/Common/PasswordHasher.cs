using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Lynx.Interfaces;

namespace Lynx.Infrastructure.Common
{
    public class PasswordHasher : IPasswordHasher
    {
        const int SaltSize = 128 / 8; // 128 bits
        public byte[] GenerateSalt()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];

            rng.GetBytes(salt);

            return salt;
        }

        public byte[] HashPassword(byte[] salt, string password)
        {
            byte[] plainText = Encoding.UTF8.GetBytes(password);

            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public bool IsPasswordVerified(byte[] salt, byte[] hashedPassword, string password)
        {
            var _hashedProvidedPass = HashPassword(salt, password);

            return _hashedProvidedPass.SequenceEqual(hashedPassword);
        }
    }
}
