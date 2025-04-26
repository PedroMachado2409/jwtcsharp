using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using jwt.Data;
using jwt.Data.Entities;
using jwt.Services.Interfaces;
using jwt.Helpers; // Vamos criar o PasswordHasher no próximo passo (se ainda não criamos)

namespace jwt.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;

        public UserService(AppDbContext dbContext, PasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterAsync(User user, string password)
        {
            if (await GetUserByUsernameAsync(user.Username!) != null)
            {
                throw new ApplicationException($"Nome de usuário '{user.Username}' já está em uso.");
            }

            if (await GetUserByEmailAsync(user.Email!) != null)
            {
                throw new ApplicationException($"O e-mail '{user.Email}' já está em uso.");
            }

            user.PasswordHash = _passwordHasher.HashPassword(password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        // Implementações para outros métodos da IUserService (UpdateUserAsync, DeleteUserAsync) viriam aqui
    }
}