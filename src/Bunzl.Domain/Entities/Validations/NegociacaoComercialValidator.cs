using FluentValidation;
using Bunzl.Infra.CrossCutting.Resources;

namespace Bunzl.Domain.Entities.Validations;

public class NegociacaoComercialValidator : AbstractValidator<NegociacaoComercial>
{
    public NegociacaoComercialValidator()
    {
        RuleFor(x => x.Codigo)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.CodigoObrigatorio);

        RuleFor(x => x.DataEntrega)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.DataEntregaObrigatorio);

        RuleFor(x => x.TermosPagamento)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.TermosPagamentoObrigatorio);
    }
}

