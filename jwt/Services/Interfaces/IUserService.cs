using jwt.Data.Entities;
using System.Threading.Tasks;

namespace jwt.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task RegisterAsync(User user, string password);
        // Podemos adicionar outras operações de usuário aqui, como:
        // Task UpdateUserAsync(User user);
        // Task DeleteUserAsync(int id);
    }
}