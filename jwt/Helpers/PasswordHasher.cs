using BCrypt.Net;

namespace jwt.Helpers
{
    public class PasswordHasher
    {
        private const int SaltRounds = 12; // Define a força do hash (quanto maior, mais seguro, mas mais lento)

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, SaltRounds);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}