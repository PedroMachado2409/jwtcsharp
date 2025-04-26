using Microsoft.EntityFrameworkCore;
using jwt.Data.Entities;
using jwt.Data.Configurations;
using jwt.Models;

namespace jwt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chamada para o método da classe base é importante.

            // Aplica a configuração do usuário.
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Aplica a configuração do produto.
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        }
    }
}
    