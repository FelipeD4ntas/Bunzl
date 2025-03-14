using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;

public class UsuarioAtualizarSenhaTelefoneValidator : AbstractValidator<UsuarioAtualizarSenhaTelefoneRequest>
{
    public UsuarioAtualizarSenhaTelefoneValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(UsuarioResources.UsuarioIdInvalido);

        RuleFor(p => p.Telefone)
            .NotEmpty()
            .WithMessage(UsuarioResources.TelefoneObrigatorio)
            .MaximumLength(30)
            .WithMessage(UsuarioResources.TelefoneMaximo30Caracteres)
            .Matches(@"^\d*$")
            .WithMessage(UsuarioResources.TelefoneApenasNumeros);

        RuleFor(p => p.Area)
            .NotEmpty()
            .WithMessage(UsuarioResources.AreaObrigatorio);

        RuleFor(p => p.SenhaAtual)
            .NotEmpty()
            .WithMessage(UsuarioResources.SenhaAtualObrigatoria);

        When(x => !string.IsNullOrEmpty(x.NovaSenha), () =>
        {
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
        });
    }
}