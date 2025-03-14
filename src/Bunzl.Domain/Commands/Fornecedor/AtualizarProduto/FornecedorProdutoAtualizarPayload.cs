using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

public class FornecedorProdutoAtualizarPayload
{
    public string CodigoFornecedor { get; set; }
    public string DescricaoCompletaFornecedor { get; set; }
    public string? DescricaoCompletaBunzl { get; set; }
    public string? AplicacoesPrincipais { get; set; }
    public string? Composicao { get; set; }
    public string? Tamanho { get; set; }
    public string? Cor { get; set; }
    public string CodigoNCM { get; set; }
    public string UnidadeMedidaFornecedorMOQ { get; set; }
    public string UnidadeMedidaFornecedorPreco { get; set; }
    public string UnidadeMedidaBunzl { get; set; }
    public int QuantidadeMinimaPedido { get; set; }
    public decimal Preco { get; set; }
    public Guid? IncotermId { get; set; }
    public string? TempoPagamento { get; set; }
    public string? Observacoes { get; set; }
    public string? DetalhesEmbalagem { get; set; }
    public decimal? PesoBruto { get; set; }
    public decimal Comprimento { get; set; }
    public decimal Largura { get; set; }
    public decimal Altura { get; set; }
    public int? TempoEntrega { get; set; }
    public decimal? CustoDesenvolvimentoEmbalagem { get; set; }
    public decimal? CustoRotulagemEmbalagem { get; set; }
    public string? PortoEmbarque { get; set; }
    public int? QuantidadeCarregamentoContainer20Ft { get; set; }
    public int? QuantidadeCarregamentoContainer40Ft { get; set; }
    public int? QuantidadeCarregamentoContainer40Hc { get; set; }
    public string? CodigoArtigo { get; set; }
    public string? Familia { get; set; }
    public string? CodigoSku { get; set; }
    public string? CorBunzl { get; set; }
    public string? TamanhoBunzl { get; set; }
    public decimal CBM { get; set; }
    public EStatusProduto Status { get; set; }
	public string? TipoEmbalagemInterna { get; set; }
	public int QuantidadePorEmbalagemInterna { get; set; }
	public string? TipoCaixaMaster { get; set; }
	public int QuantidadePorCaixaMaster { get; set; }
	public decimal CapacidadeMensalFabrica { get; set; }
	public string? UnidadeMedidaCapacidadeMensal { get; set; }
	public string? NomeFabrica { get; set; }
	public decimal CustoDetalhadoMateriaPrima { get; set; }
	public decimal CustoDetalhadoCombustivel { get; set; }
	public decimal CustoDetalhadoEmbalagem { get; set; }
	public decimal CustoDetalhadoMaoDeObra { get; set; }
	public decimal CustoDetalhadoEnergia { get; set; }
	public decimal CustoDetalhadoTransporte { get; set; }

	public FornecedorProdutoAtualizarRequest ToRequest(Guid fornecedorId, Guid produtoId)
    {
        return new FornecedorProdutoAtualizarRequest(
            produtoId,
            fornecedorId,
            CodigoFornecedor,
            DescricaoCompletaFornecedor,
            DescricaoCompletaBunzl,
            AplicacoesPrincipais,
            Composicao,
            Tamanho,
            Cor,
            CodigoNCM,
            UnidadeMedidaFornecedorMOQ,
            UnidadeMedidaFornecedorPreco,
            UnidadeMedidaBunzl,
            QuantidadeMinimaPedido,
            Preco,
            IncotermId,
            TempoPagamento,
            Observacoes,
            DetalhesEmbalagem,
            PesoBruto,
            Comprimento,
            Largura,
            Altura,
            TempoEntrega,
            CustoDesenvolvimentoEmbalagem,
            CustoRotulagemEmbalagem,
            PortoEmbarque,
            QuantidadeCarregamentoContainer20Ft,
            QuantidadeCarregamentoContainer40Ft,
            QuantidadeCarregamentoContainer40Hc,
            CodigoArtigo,
            Familia,
            CodigoSku,
            CorBunzl,
            TamanhoBunzl,
            CBM,
            Status,
			TipoEmbalagemInterna,
			QuantidadePorEmbalagemInterna,
			TipoCaixaMaster,
			QuantidadePorCaixaMaster,
			CapacidadeMensalFabrica,
			UnidadeMedidaCapacidadeMensal,
			NomeFabrica,
			CustoDetalhadoMateriaPrima,
			CustoDetalhadoCombustivel,
			CustoDetalhadoEmbalagem,
			CustoDetalhadoMaoDeObra,
			CustoDetalhadoEnergia,
			CustoDetalhadoTransporte
		);
    }
}
