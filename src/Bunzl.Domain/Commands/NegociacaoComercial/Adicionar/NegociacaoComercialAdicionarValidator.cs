using Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;
using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;

public class NegociacaoComercialAtualizarValidator : AbstractValidator<NegociacaoComercialAtualizarRequest>
{
    public NegociacaoComercialAtualizarValidator()
    {

        RuleFor(x => x.DataEntrega)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.DataEntregaObrigatorio);

        RuleFor(x => x.TermosPagamento)
            .NotEmpty()
            .WithMessage(NegociacaoComercialResources.TermosPagamentoObrigatorio);
	}
}

