using CleanGo.Application.Interfaces.Security;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CleanGo.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 10000;

        public string HashPassword(string plainPassword)
        {
            // Create salt.
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derive hash.
            var key = KeyDerivation.Pbkdf2(
                password: plainPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize
            );

            // Save format: {iterations}.{salt}.{key}.
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            var parts = hashedPassword.Split('.', 3);
            if (parts.Length != 3)
                return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            // Recalculate hash.
            var newKey = KeyDerivation.Pbkdf2(
                plainPassword,
                salt,
                KeyDerivationPrf.HMACSHA256,
                iterations,
                KeySize
            );

            return CryptographicOperations.FixedTimeEquals(newKey, key);
        }
    }
}
