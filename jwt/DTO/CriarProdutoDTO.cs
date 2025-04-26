using System.ComponentModel.DataAnnotations;

namespace DTO
    {
       
        public class CriarProdutoDTO
        {
            [Required(ErrorMessage = "O nome do produto é obrigatório.")]
            [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
            [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
            public string Descricao { get; set; }

            [Required(ErrorMessage = "O preço do produto é obrigatório.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
            public decimal Preco { get; set; }

            [Required(ErrorMessage = "A quantidade em estoque do produto é obrigatória.")]
            [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero.")]
            public int QuantidadeEstoque { get; set; }
        }
    }