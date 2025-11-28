using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using UchebBirzha.Infrastructure.Interfaces;

namespace UchebBirzha.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
           
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

           
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            
            byte[] hashBytes = new byte[salt.Length + hashed.Length];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hashed, 0, hashBytes, salt.Length, hashed.Length);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(passwordHash);

                
                byte[] salt = new byte[128 / 8];
                Array.Copy(hashBytes, 0, salt, 0, salt.Length);

                
                byte[] storedHash = new byte[hashBytes.Length - salt.Length];
                Array.Copy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

                
                byte[] computedHash = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8);

                
                return computedHash.SequenceEqual(storedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}