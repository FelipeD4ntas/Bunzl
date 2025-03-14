using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;
namespace Bunzl.Domain.Entities.Validations;

public class FornecedorProdutoValidator : AbstractValidator<FornecedorProduto>
{
    public FornecedorProdutoValidator() 
    {
        RuleFor(p => p.FornecedorId)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoFornecedorIdObrigatorio);

        RuleFor(p => p.CodigoFornecedor)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoCodigoFornecedorObrigatorio);

        RuleFor(p => p.DescricaoCompletaFornecedor)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoDescricaoCompletaFornecedorObrigatorio);

        RuleFor(p => p.CodigoNCM)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoCodigoNCMObrigatorio);

        RuleFor(p => p.UnidadeMedidaFornecedorMOQ)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorMOQObrigatorio);

        RuleFor(p => p.UnidadeMedidaFornecedorPreco)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorPrecoObrigatorio);

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoPrecoMenorOuIgualZero); 

        RuleFor(p => p.Comprimento)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoComprimentoMenorOuIgualZero);

        RuleFor(p => p.Largura)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoLarguraMenorOuIgualZero);

        RuleFor(p => p.Altura)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoAlturaMenorOuIgualZero);
    }
}
