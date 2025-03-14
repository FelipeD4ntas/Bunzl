using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;

public class LoginAlterarEmpresaValidator : AbstractValidator<LoginAlterarEmpresaRequest>
{
    public LoginAlterarEmpresaValidator()
    {
        RuleFor(p => p.EmpresaId)
            .NotNull()
            .WithMessage(LoginResources.LoginEmpresaInvalida)
            .NotEmpty()
            .WithMessage(LoginResources.LoginEmpresaInvalida);
    }
}