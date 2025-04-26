using jwt.Models.Request;
using jwt.Models.Response;
using jwt.Data.Entities;

namespace jwt.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse?> Authenticate(LoginRequest model);
        string GenerateToken(User user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}