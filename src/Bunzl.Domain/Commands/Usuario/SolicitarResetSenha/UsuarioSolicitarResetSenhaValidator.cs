using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;

public class UsuarioSolicitarResetSenhaValidator : AbstractValidator<UsuarioSolicitarResetSenhaRequest>
{
    public UsuarioSolicitarResetSenhaValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(UsuarioResources.EmailObrigatorio)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(UsuarioResources.EmailInvalido)
            .MaximumLength(250)
            .WithMessage(UsuarioResources.EmailMaximo250Caracteres);
    }
}