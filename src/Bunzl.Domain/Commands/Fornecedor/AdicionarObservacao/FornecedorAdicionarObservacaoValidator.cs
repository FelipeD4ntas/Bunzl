using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;

public class FornecedorAdicionarObservacaoValidator : AbstractValidator<FornecedorAdicionarObservacaoRequest>
{
    public FornecedorAdicionarObservacaoValidator()
    {
        RuleFor(p => p.Observacao)
            .NotEmpty()
            .WithMessage(FornecedorResources.ObservacaoObrigatoria)
            .MaximumLength(500)
            .WithMessage(FornecedorResources.ObservacaoMaximo500Caracteres);
    }
}