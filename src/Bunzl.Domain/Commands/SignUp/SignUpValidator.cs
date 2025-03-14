using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
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

    }
}