namespace jwt.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using jwt.Models; // Importe o namespace onde a classe Produto está definida

    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Configura o nome da tabela
            builder.ToTable("Produtos");

            // Configura a chave primária
            builder.HasKey(p => p.Id);

            // Configura as propriedades
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Define o tipo de dado para decimal

            builder.Property(p => p.QuantidadeEstoque)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); // Configura o valor padrão para a data UTC no SQL Server (ajuste para o seu SGBD)
            
             // Exemplo de configuração de índice
             builder.HasIndex(p => p.Nome);
        }
    }
}