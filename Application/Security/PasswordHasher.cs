using System.Security.Cryptography;

namespace Application.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32;  // 256 bits
        private const int Iterations = 100_000;

        public static (string hash, string salt) HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = deriveBytes.GetBytes(KeySize);

            return (Convert.ToBase64String(key), Convert.ToBase64String(salt));
        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var keyToCheck = deriveBytes.GetBytes(KeySize);

            var originalKey = Convert.FromBase64String(hash);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, originalKey);
        }
    }
}
