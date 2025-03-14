using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.Usuario.Atualizar;

public class UsuarioAtualizarValidator : AbstractValidator<UsuarioAtualizarRequest>
{
    public UsuarioAtualizarValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage(UsuarioResources.NomeObrigatorio)
            .MaximumLength(250)
            .WithMessage(UsuarioResources.NomeMaximo250Caracteres)
            .When(p => !string.IsNullOrEmpty(p.Nome));

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(UsuarioResources.EmailObrigatorio)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(UsuarioResources.EmailInvalido)
            .MaximumLength(250)
            .WithMessage(UsuarioResources.EmailMaximo250Caracteres)
            .When(p => !string.IsNullOrEmpty(p.Email));
    }
}