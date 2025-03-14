using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;

public class NegociacaoComercialAdicionarObservacaoValidator : AbstractValidator<NegociacaoComercialAdicionarObservacaoRequest>
{
    public NegociacaoComercialAdicionarObservacaoValidator()
    {
        RuleFor(p => p.Observacao)
            .MaximumLength(500)
            .WithMessage(NegociacaoComercialResources.ObservacaoDeveConterNoMaximo500Caracteres);
    }
}

