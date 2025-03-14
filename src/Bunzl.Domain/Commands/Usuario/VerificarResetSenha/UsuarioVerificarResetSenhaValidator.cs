using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Usuario.VerificarResetSenha;

public class UsuarioVerificarResetSenhaValidator : AbstractValidator<UsuarioVerificarResetSenhaRequest>
{
    public UsuarioVerificarResetSenhaValidator()
    {
        RuleFor(p => p.Chave)
            .NotEmpty()
            .WithMessage(UsuarioResources.ChaveResetSenhaObrigatorio);
    }
}

