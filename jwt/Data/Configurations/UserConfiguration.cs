using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using jwt.Data.Entities;

namespace jwt.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Configurando o nome da tabela (opcional, por convenção é o nome da entidade no plural)
            builder.ToTable("Users");

            // Configurando a chave primária (já definida na entidade com [Key], mas pode ser explicitado aqui)
            builder.HasKey(u => u.Id);

            // Configurando índices únicos para Username e Email
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            // Configurando as propriedades e suas restrições (algumas já foram definidas com DataAnnotations na entidade)
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()"); // Define o valor padrão para a hora atual no PostgreSQL

            builder.Property(u => u.UpdatedAt)
                .IsRequired(false); // Permite valores nulos para UpdatedAt

            // Exemplo de configuração para a propriedade Role (se você a adicionou na entidade)
            // builder.Property(u => u.Role)
            //     .HasMaxLength(20)
            //     .HasDefaultValue("User");

            // Você pode adicionar outros mapeamentos e configurações aqui, como relacionamentos com outras entidades.
        }
    }
}