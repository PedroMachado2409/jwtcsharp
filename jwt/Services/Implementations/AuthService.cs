using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using jwt.Data.Entities;
using jwt.Models.Request;
using jwt.Models.Response;
using jwt.Services.Interfaces;
using jwt.Settings;
using jwt.Helpers; // Vamos criar o JwtHelper no próximo passo

namespace jwt.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IUserService userService, IOptions<JwtSettings> jwtSettings, PasswordHasher passwordHasher, JwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponse?> Authenticate(LoginRequest model)
        {
            var user = await _userService.GetUserByUsernameAsync(model.Username!);

            if (user == null || !_passwordHasher.VerifyPassword(model.Password!, user.PasswordHash!))
            {
                return null; // Falha na autenticação
            }

            var token = _jwtHelper.GenerateToken(user, _jwtSettings);
            return new AuthResponse { AccessToken = token };
        }

        public string GenerateToken(User user)
        {
            // Este método não será usado diretamente aqui, pois a lógica de geração do token
            // foi movida para o JwtHelper para melhor separação de responsabilidades.
            throw new NotImplementedException("Use o JwtHelper para gerar tokens.");
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return _passwordHasher.VerifyPassword(password, hashedPassword);
        }
    }
}