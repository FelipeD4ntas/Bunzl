using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;

public class LoginGerarCodigoOtpValidator : AbstractValidator<LoginGerarCodigoOtpRequest>
{
    public LoginGerarCodigoOtpValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage(LoginResources.LoginInvalido)
            .NotEmpty()
            .WithMessage(LoginResources.LoginInvalido);
    }
}