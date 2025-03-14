using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Usuario.ResetSenha;

public class UsuarioResetSenhaValidator : AbstractValidator<UsuarioResetSenhaRequest>
{
    public UsuarioResetSenhaValidator()
    {
        RuleFor(p => p.Chave)
            .NotEmpty()
            .WithMessage(UsuarioResources.ChaveResetSenhaObrigatorio);

        RuleFor(p => p.NovaSenha)
            .NotEmpty()
            .WithMessage(UsuarioResources.SenhaObrigatoria)
            .Length(8, 12)
            .WithMessage(UsuarioResources.SenhaDeveTerTamanho8a12)
            .Matches(@"[A-Z]")
            .WithMessage(UsuarioResources.SenhaDeveTerMaiscula)
            .Matches(@"[a-z]")
            .WithMessage(UsuarioResources.SenhaDeveTerMinuscula)
            .Matches(@"\d")
            .WithMessage(UsuarioResources.SenhaDeveTerNumero)
            .Matches(@"[\W_]")
            .WithMessage(UsuarioResources.SenhaDeveTerCaracterEspecial);

        RuleFor(p => p.ConfirmacaoNovaSenha)
            .NotEmpty()
            .WithMessage(UsuarioResources.SenhaConfirmacaoObrigatoria)
            .Equal(p => p.NovaSenha)
            .WithMessage(UsuarioResources.SenhaConfirmacaoNaoConfere);
    }
}

