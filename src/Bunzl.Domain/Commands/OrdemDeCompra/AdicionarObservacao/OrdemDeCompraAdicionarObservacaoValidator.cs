using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarObservacao;

public class OrdemDeCompraAdicionarObservacaoValidator : AbstractValidator<OrdemDeCompraAdicionarObservacaoRequest>
{
    public OrdemDeCompraAdicionarObservacaoValidator()
    {
        RuleFor(p => p.Observacao)
            .MaximumLength(500)
            .WithMessage(OrdemDeCompraResources.ObservacaoDeveConterNoMaximo500Caracteres);
    }
}
