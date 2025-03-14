using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;

public class FornecedorProdutoAtualizarRequest(
    Guid id,
    Guid fornecedorId,
    string codigoFornecedor,
    string descricaoCompletaFornecedor,
    string? descricaoCompletaBunzl,
    string? aplicacoesPrincipais,
    string? composicao,
    string? tamanho,
    string? cor,
    string codigoNCM,
    string unidadeMedidaFornecedorMOQ,
    string unidadeMedidaFornecedorPreco,
    string unidadeMedidaBunzl,
    int quantidadeMinimaPedido,
    decimal preco,
    Guid? incotermId,
    string? termoPagamento,
    string? observacoes,
    string? detalhesEmbalagem,
    decimal? pesoBruto,
    decimal comprimento,
    decimal largura,
    decimal altura,
    int? tempoEntrega,
    decimal? custoDesenvolvimentoEmbalagem,
    decimal? custoRotulagemEmbalagem,
    string? portoEmbarque,
    int? quantidadeCarregamentoContainer20Ft,
    int? quantidadeCarregamentoContainer40Ft,
    int? quantidadeCarregamentoContainer40Hc,
    string? codigoArtigo,
    string? familia,
    string? codigoSku,
    string? corBunzl,
    string? tamanhoBunzl,
    decimal cBM,
	EStatusProduto status,
	string? tipoEmbalagemInterna,
	int quantidadePorEmbalagemInterna,
	string? tipoCaixaMaster,
	int quantidadePorCaixaMaster,
	decimal capacidadeMensalFabrica,
	string? unidadeMedidaCapacidadeMensal,
	string? nomeFabrica,
	decimal custoDetalhadoMateriaPrima,
	decimal custoDetalhadoCombustivel,
	decimal custoDetalhadoEmbalagem,
	decimal custoDetalhadoMaoDeObra,
	decimal custoDetalhadoEnergia,
	decimal custoDetalhadoTransporte
    ) : IRequest<CommandResponse<FornecedorProdutoAtualizarResponse>>
{
    public Guid Id { get; set; } = id;
    public Guid FornecedorId { get; set; } = fornecedorId;
    public string CodigoFornecedor { get; set; } = codigoFornecedor;
    public string DescricaoCompletaFornecedor { get; set; } = descricaoCompletaFornecedor;
    public string? DescricaoCompletaBunzl { get; set; } = descricaoCompletaBunzl;
    public string? AplicacoesPrincipais { get; set; } = aplicacoesPrincipais;
    public string? Composicao { get; set; } = composicao;
    public string? Tamanho { get; set; } = tamanho;
    public string? Cor { get; set; } = cor;
    public string CodigoNCM { get; set; } = codigoNCM;
    public string UnidadeMedidaFornecedorMOQ { get; set; } = unidadeMedidaFornecedorMOQ;
    public string UnidadeMedidaFornecedorPreco { get; set; } = unidadeMedidaFornecedorPreco;
    public string UnidadeMedidaBunzl { get; set; } = unidadeMedidaBunzl;
    public int QuantidadeMinimaPedido { get; set; } = quantidadeMinimaPedido;
    public decimal Preco { get; set; } = preco;
    public Guid? IncotermId { get; set; } = incotermId;
    public string? TermoPagamento { get; set; } = termoPagamento;
    public string? Observacoes { get; set; } = observacoes;
    public string? DetalhesEmbalagem { get; set; } = detalhesEmbalagem;
    public decimal? PesoBruto { get; set; } = pesoBruto;
    public decimal Comprimento { get; set; } = comprimento;
    public decimal Largura { get; set; } = largura;
    public decimal Altura { get; set; } = altura;
    public int? TempoEntrega { get; set; } = tempoEntrega;
    public decimal? CustoDesenvolvimentoEmbalagem { get; set; } = custoDesenvolvimentoEmbalagem;
    public decimal? CustoRotulagemEmbalagem { get; set; } = custoRotulagemEmbalagem;
    public string? PortoEmbarque { get; set; } = portoEmbarque;
    public int? QuantidadeCarregamentoContainer20Ft { get; set; } = quantidadeCarregamentoContainer20Ft;
    public int? QuantidadeCarregamentoContainer40Ft { get; set; } = quantidadeCarregamentoContainer40Ft;
    public int? QuantidadeCarregamentoContainer40Hc { get; set; } = quantidadeCarregamentoContainer40Hc;
    public string? CodigoArtigo { get; set; } = codigoArtigo;
    public string? Familia { get; set; } = familia;
    public string? CodigoSku { get; set; } = codigoSku;
    public string? CorBunzl { get; set; } = corBunzl;
    public string? TamanhoBunzl { get; set; } = tamanhoBunzl;
    public decimal CBM { get; set; } = cBM;
    public EStatusProduto Status { get; set; } = status;
	public string? TipoEmbalagemInterna { get; set; } = tipoEmbalagemInterna;
	public int QuantidadePorEmbalagemInterna { get; set; } = quantidadePorEmbalagemInterna;
	public string? TipoCaixaMaster { get; set; } = tipoCaixaMaster;
	public int QuantidadePorCaixaMaster { get; set; } = quantidadePorCaixaMaster;
	public decimal CapacidadeMensalFabrica { get; set; } = capacidadeMensalFabrica;
	public string? UnidadeMedidaCapacidadeMensal { get; set; } = unidadeMedidaCapacidadeMensal;
	public string? NomeFabrica { get; set; } = nomeFabrica;
	public decimal CustoDetalhadoMateriaPrima { get; set; } = custoDetalhadoMateriaPrima;
	public decimal CustoDetalhadoCombustivel { get; set; } = custoDetalhadoCombustivel;
	public decimal CustoDetalhadoEmbalagem { get; set; } = custoDetalhadoEmbalagem;
	public decimal CustoDetalhadoMaoDeObra { get; set; } = custoDetalhadoMaoDeObra;
	public decimal CustoDetalhadoEnergia { get; set; } = custoDetalhadoEnergia;
	public decimal CustoDetalhadoTransporte { get; set; } = custoDetalhadoTransporte;
}
