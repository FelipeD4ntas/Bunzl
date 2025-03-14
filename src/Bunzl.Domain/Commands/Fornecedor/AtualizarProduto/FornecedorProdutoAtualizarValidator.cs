using Bunzl.Infra.CrossCutting.Resources;
using FluentValidation;

namespace Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

public class FornecedorProdutoAtualizarValidator : AbstractValidator<FornecedorProdutoAtualizarRequest>
{
    public FornecedorProdutoAtualizarValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoIdObrigatorio);

        RuleFor(p => p.FornecedorId)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoFornecedorIdObrigatorio);

        RuleFor(p => p.CodigoFornecedor)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoCodigoFornecedorObrigatorio)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoCodigoFornecedorMaximo50Car);

        RuleFor(p => p.DescricaoCompletaFornecedor)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoDescricaoCompletaFornecedorObrigatorio)
            .MaximumLength(500).WithMessage(FornecedorResources.ProdutoDescricaoCompletaFornecedorMaximo500Car);

        RuleFor(p => p.DescricaoCompletaBunzl)
            .MaximumLength(500).WithMessage(FornecedorResources.ProdutoDescricaoCompletaBunzlMaximo500Car);

        RuleFor(p => p.AplicacoesPrincipais)
            .MaximumLength(500).WithMessage(FornecedorResources.ProdutoAplicacoesPrincipaisMaximo500Car);

        RuleFor(p => p.Composicao)
            .MaximumLength(200).WithMessage(FornecedorResources.ProdutoComposicaoMaximo200Car);

        RuleFor(p => p.Tamanho)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoTamanhoMaximo50Car);

        RuleFor(p => p.Cor)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoCorMaximo50Car);

        RuleFor(p => p.CodigoNCM)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoCodigoNCMObrigatorio)
            .MaximumLength(10).WithMessage(FornecedorResources.ProdutoCodigoNCMMaximo10Car);

        RuleFor(p => p.UnidadeMedidaFornecedorMOQ)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorMOQObrigatorio)
            .MaximumLength(10).WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorMOQMaximo10Car);

        RuleFor(p => p.UnidadeMedidaFornecedorPreco)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorPrecoObrigatorio)
            .MaximumLength(10).WithMessage(FornecedorResources.ProdutoUnidadeMedidaFornecedorPrecoMaximo10Car);

        RuleFor(p => p.UnidadeMedidaBunzl)
            .MaximumLength(10).WithMessage(FornecedorResources.ProdutoUnidadeMedidaBunzlMaximo10Car);

        RuleFor(p => p.TermoPagamento)
            .MaximumLength(300).WithMessage(FornecedorResources.ProdutoTermoPagamentoMaximo300Car);

        RuleFor(p => p.Observacoes)
            .MaximumLength(1000).WithMessage(FornecedorResources.ProdutoObservacoesMaximo1000Car);

        RuleFor(p => p.DetalhesEmbalagem)
            .MaximumLength(500).WithMessage(FornecedorResources.ProdutoDetalhesEmbalagemMaximo500Car);

        RuleFor(p => p.PortoEmbarque)
            .MaximumLength(100).WithMessage(FornecedorResources.ProdutoPontoEmbarqueMaximo100Car);

        RuleFor(p => p.CodigoArtigo)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoCodigoArtigoMaximo50Car);

        RuleFor(p => p.Familia)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoFamiliaMaximo50Car);

        RuleFor(p => p.CodigoSku)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoCodigoSkuMaximo50Car);

        RuleFor(p => p.CorBunzl)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoCorBunzlMaximo50Car);

        RuleFor(p => p.TamanhoBunzl)
            .MaximumLength(50).WithMessage(FornecedorResources.ProdutoTamanhoBunzlMaximo50Car);

        RuleFor(p => p.Preco)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoPrecoObrigatorio)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoPrecoMenorOuIgualZero);

        RuleFor(p => p.PesoBruto)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoPesoBrutoMenorOuIgualZero);

        RuleFor(p => p.Comprimento)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoComprimentoObrigatorio)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoComprimentoMenorOuIgualZero);

        RuleFor(p => p.Largura)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoLarguraObrigatorio)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoLarguraMenorOuIgualZero);

        RuleFor(p => p.Altura)
            .NotEmpty().WithMessage(FornecedorResources.ProdutoAlturaObrigatorio)
            .GreaterThan(0).WithMessage(FornecedorResources.ProdutoAlturaMenorOuIgualZero);

        RuleFor(p => p.TempoEntrega)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoTempoEntregaMenorOuIgualZero);

        RuleFor(p => p.CustoDesenvolvimentoEmbalagem)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoCustoDesenvolvimentoEmbalagemMenorOuIgualZero);

        RuleFor(p => p.CustoRotulagemEmbalagem)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoCustoRotulagemEmbalagemMenorOuIgualZero);

        RuleFor(p => p.QuantidadeCarregamentoContainer20Ft)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoQuantidadeCarregamentoContainer20FtMenorOuIgualZero);

        RuleFor(p => p.QuantidadeCarregamentoContainer40Ft)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoQuantidadeCarregamentoContainer40FtMenorOuIgualZero);

        RuleFor(p => p.QuantidadeCarregamentoContainer40Hc)
            .GreaterThanOrEqualTo(0).WithMessage(FornecedorResources.ProdutoQuantidadeCarregamentoContainer40HcMenorOuIgualZero);
    }
}
