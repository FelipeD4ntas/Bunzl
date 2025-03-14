using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class NegociacaoComercialProduto : EntityBase<Guid>
{
    public Guid ProdutoId { get; set; }
    public string CodigoSku { get; set; }
    public string Descricao { get; set; }
    public decimal Quantidade { get; set; }
    public decimal ValorUnitarioOriginal { get; set; }
    public decimal ValorUnitarioAlvo { get; set; }
    public decimal ValorUnitarioNegociado { get; set; }
    public decimal ValorUnitarioFinal { get; set; }
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public Guid NegociacaoComercialId { get; set; }
    public virtual NegociacaoComercial? NegociacaoComercial { get; set; }

    protected NegociacaoComercialProduto()
    {
    }

    public NegociacaoComercialProduto(
        Guid produtoId, 
        string sku, 
        string descricao, 
        decimal valor, 
        decimal quantidade, 
        decimal valorSugerido, 
        string? observacao, 
        Guid negociacaoComercialId,
        decimal valorUnitarioAlvo,
        decimal valorUnitarioFinal)
    {
        ProdutoId = produtoId;
        CodigoSku = sku;
        Descricao = descricao;
        Quantidade = quantidade;
        ValorUnitarioOriginal = valor;
        ValorUnitarioNegociado = valorSugerido;
        ValorUnitarioFinal = valorUnitarioFinal;
        ValorTotal = quantidade * valorSugerido;
        ValorUnitarioAlvo = valorUnitarioAlvo;
        Observacao = observacao;
        NegociacaoComercialId = negociacaoComercialId;
    }
}

