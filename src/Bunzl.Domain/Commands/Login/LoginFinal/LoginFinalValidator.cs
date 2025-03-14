using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Login.LoginFinal;

public class LoginFinalValidator : AbstractValidator<LoginFinalRequest>
{
    public LoginFinalValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage(LoginResources.LoginInvalido)
            .NotEmpty()
            .WithMessage(LoginResources.LoginInvalido);

        RuleFor(x => x.CodigoOtp)
            .NotNull()
            .WithMessage(LoginResources.LoginCodigoOtpInvalido)
            .NotEmpty()
            .WithMessage(LoginResources.LoginCodigoOtpInvalido)
            .Length(6)
            .WithMessage(LoginResources.LoginCodigoOtp6CaracateresNumericos)
            .Matches(@"^\d{6}$")
            .WithMessage(LoginResources.LoginCodigoOtp6CaracateresNumericos);
    }
}