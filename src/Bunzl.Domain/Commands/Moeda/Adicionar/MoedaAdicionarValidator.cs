using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Moeda.Adicionar;

public class MoedaAdicionarValidator : AbstractValidator<MoedaAdicionarRequest>
{
    public MoedaAdicionarValidator()
    {
        RuleFor(p => p.Sigla)
            .NotEmpty()
            .WithMessage(MoedaResources.SiglaObrigatoria)
            .MaximumLength(5)
            .WithMessage(MoedaResources.SiglaMaximo5Caracteres);

        RuleFor(p => p.Descricao)
            .NotEmpty()
            .WithMessage(MoedaResources.DescricaoObrigatoria)
            .MaximumLength(100)
            .WithMessage(MoedaResources.DescricaoMaximo100Caracteres);

    }
}