using System.ComponentModel.DataAnnotations;

namespace jwt.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Password { get; set; }
    }
}