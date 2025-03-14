using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.Usuario.Adicionar;

public class UsuarioAdicionarValidator : AbstractValidator<UsuarioAdicionarRequest>
{
    public UsuarioAdicionarValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage(UsuarioResources.NomeObrigatorio)
            .MaximumLength(250)
            .WithMessage(UsuarioResources.NomeMaximo250Caracteres);

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(UsuarioResources.EmailObrigatorio)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(UsuarioResources.EmailInvalido)
            .MaximumLength(250)
            .WithMessage(UsuarioResources.EmailMaximo250Caracteres);

        RuleFor(p => p.PerfilPermissao)
            .NotEmpty()
            .WithMessage(UsuarioResources.PerfilPermissaoObrigatorio)
            .IsInEnum()
            .WithMessage(UsuarioResources.PerfilInvalido);

        RuleFor(p => p.EmpresasId)
            .NotEmpty()
            .WithMessage(UsuarioResources.EmpresaObrigatoria);
    }
}