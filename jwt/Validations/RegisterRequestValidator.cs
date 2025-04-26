using FluentValidation;
using jwt.Models.Request;

namespace jwt.Validations.FluentValidation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .Length(3, 50).WithMessage("O nome de usuário deve ter entre 3 e 50 caracteres.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail é inválido.")
                .MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("A confirmação da senha é obrigatória.")
                .Equal(p => p.Password).WithMessage("A senha e a confirmação não coincidem.");
        }
    }
}