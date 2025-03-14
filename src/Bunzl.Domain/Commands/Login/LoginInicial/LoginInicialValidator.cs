using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
using FluentValidation.Validators;

namespace Bunzl.Domain.Commands.Login.LoginInicial;

public class LoginInicialValidator : AbstractValidator<LoginInicialRequest>
{
    public LoginInicialValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(LoginResources.EmailObrigatorio)
            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
            .WithMessage(LoginResources.EmailInvalido);

        RuleFor(p => p.Senha)
            .NotEmpty()
            .WithMessage(LoginResources.SenhaObrigatoria);
    }
}