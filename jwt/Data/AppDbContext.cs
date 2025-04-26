using Microsoft.EntityFrameworkCore;
using jwt.Data.Entities;
using jwt.Data.Configurations;

namespace jwt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicando as configurações das entidades que criamos
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            // Você pode adicionar outras configurações de entidades aqui no futuro
            // modelBuilder.ApplyConfiguration(new OutraEntidadeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}