using System.ComponentModel.DataAnnotations;

namespace jwt.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O nome de usuário deve ter no máximo 50 caracteres.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        [MaxLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não coincidem.")]
        public string? ConfirmPassword { get; set; }
    }
}