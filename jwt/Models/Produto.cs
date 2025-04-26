
using System.ComponentModel.DataAnnotations;


// Namespace para o projeto
namespace jwt
{
    // Namespace para a camada de Models
    namespace Models
    {
        // Classe que representa a entidade Produto no banco de dados
        public class Produto
        {
            [Key] // Define que o Id é a chave primária
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome do produto é obrigatório.")] // Define que o nome é obrigatório
            [MaxLength(200, ErrorMessage = "O nome do produto deve ter no máximo 200 caracteres.")] // Define o tamanho máximo do nome
            public string Nome { get; set; }

            [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
            [MaxLength(1000, ErrorMessage = "A descrição do produto deve ter no máximo 1000 caracteres.")]
            public string Descricao { get; set; }

            [Required(ErrorMessage = "O preço do produto é obrigatório.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")] // Define que o preço deve ser maior que zero
            public decimal Preco { get; set; }

            [Required(ErrorMessage = "A quantidade em estoque do produto é obrigatória.")]
            [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero.")]
            public int QuantidadeEstoque { get; set; }

            public DateTime DataCadastro { get; set; } = DateTime.UtcNow; // Define a data de cadastro como a data atual por padrão

            // Propriedade de navegação para a categoria (caso você tenha categorias)
            // [JsonIgnore] // Opcional: Ignora a propriedade na serialização JSON para evitar loops
            // public Categoria Categoria { get; set; }
            // public int CategoriaId { get; set; }
        }
    }
}