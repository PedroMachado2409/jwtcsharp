using FluentValidation;
using jwt.Models.Request;

namespace jwt.Validations.FluentValidation
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .Length(3, 50).WithMessage("O nome de usuário deve ter entre 3 e 50 caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}