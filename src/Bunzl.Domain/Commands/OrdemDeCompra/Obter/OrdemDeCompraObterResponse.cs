using Bunzl.Domain.Enumerators;
using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Obter;

public class OrdemDeCompraObterResponse(
    Guid id,
    Guid fornecedorId,
    Guid empresaId,
    string? codigoFornecedor,
    string? codigoErpFornecedor,
    string? paisImportador,
    string? numeroOrdem,
    DateTime? dataOrdem,
    string? numeroRevisao,
    DateTime? dataRevisao,
    string? codigoEstabelecimento,
    DateTime? dataExp,
    string? nomeFornecedor,
    string? enderecoFornecedor,
    string? numeroEnderecoFornecedor,
    string? contatoFornecedor,
    string? emailFornecedor,
    string? codigoFabricante,
    string? nomeFabricante,
    string? enderecoFabricante,
    string? nomeImportador,
    string? enderecoImportador,
    string? contatoImportador,
    string? emailImportador,
    string? numeroEnderecoImportador,
    string? complementoEnderecoImportador,
    string? bairroEnderecoImportador,
    string? estadoProvinciaImportador,
    string? zipCodeImportador,
    string? cnpjImportador,
    string? prazoPagamento,
    string? tipoFrete,
    string? modoEntrega,
    string? nomeAgente,
    string? destino,
    int? numeroContainer20,
    int? numeroContainer40,
    int? numeroContainer40HC,
    int? numeroContainerOutros,
    decimal? totalCBM,
    decimal? pesoTotal,
    string? nomeComprador,
    string? nomeVendedor,
    DateTime? dataAlteracao,
    decimal? taxaEmbalagem,
    decimal? taxaInterna,
    decimal? outrasDespesas,
    decimal? desconto,
    decimal? frete,
    string? acordoFornecimento,
    decimal? valorTotal,
    EStatusOrdemDeCompra status,
    List<OrdemDeCompraProdutoDto>? produtos,
    List<OrdemDeCompraAnexoDto>? anexos,
    List<OrdemDeCompraObservacaoDto>? observacoes,
    List<OrdemDeCompraUnidadeMedidaDto>? unidadesMedida
	)
{
    public Guid Id { get; set; } = id;
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid EmpresaId { get; set; } = empresaId;
    public string? CodigoFornecedor { get; set; } = codigoFornecedor;
    public string? CodigoErpFornecedor { get; set; } = codigoErpFornecedor;
    public string? PaisImportador { get; set; } = paisImportador;
    public string? NumeroOrdem { get; set; } = numeroOrdem;
    public DateTime? DataOrdem { get; set; } = dataOrdem;
    public string? NumeroRevisao { get; set; } = numeroRevisao;
    public DateTime? DataRevisao { get; set; } = dataRevisao;
    public string? CodigoEstabelecimento { get; set; } = codigoEstabelecimento;
    public DateTime? DataExp { get; set; } = dataExp;
    public string? NomeFornecedor { get; set; } = nomeFornecedor;
    public string? EnderecoFornecedor { get; set; } = enderecoFornecedor;
    public string? NumeroEnderecoFornecedor { get; set; } = numeroEnderecoFornecedor;
    public string? ContatoFornecedor { get; set; } = contatoFornecedor;
    public string? EmailFornecedor { get; set; } = emailFornecedor;
    public string? CodigoFabricante { get; set; } = codigoFabricante;
    public string? NomeFabricante { get; set; } = nomeFabricante;
    public string? EnderecoFabricante { get; set; } = enderecoFabricante;
    public string? NomeImportador { get; set; } = nomeImportador;
    public string? EnderecoImportador { get; set; } = enderecoImportador;
    public string? ContatoImportador { get; set; } = contatoImportador;
    public string? EmailImportador { get; set; } = emailImportador;
    public string? NumeroEnderecoImportador { get; set; } = numeroEnderecoImportador;
    public string? ComplementoEnderecoImportador { get; set; } = complementoEnderecoImportador;
    public string? BairroEnderecoImportador { get; set; } = bairroEnderecoImportador;
    public string? EstadoProvinciaImportador { get; set; } = estadoProvinciaImportador;
    public string? ZipCodeImportador { get; set; } = zipCodeImportador;
    public string? CnpjImportador { get; set; } = cnpjImportador;
    public string? PrazoPagamento { get; set; } = prazoPagamento;
    public string? TipoFrete { get; set; } = tipoFrete;
    public string? ModoEntrega { get; set; } = modoEntrega;
    public string? NomeAgente { get; set; } = nomeAgente;
    public string? Destino { get; set; } = destino;
    public int? NumeroContainer20 { get; set; } = numeroContainer20;
    public int? NumeroContainer40 { get; set; } = numeroContainer40;
    public int? NumeroContainer40HC { get; set; } = numeroContainer40HC;
    public int? NumeroContainerOutros { get; set; } = numeroContainerOutros;
    public decimal? TotalCBM { get; set; } = totalCBM;
    public decimal? PesoTotal { get; set; } = pesoTotal;
    public string? NomeComprador { get; set; } = nomeComprador;
    public string? NomeVendedor { get; set; } = nomeVendedor;
    public DateTime? DataAlteracao { get; set; } = dataAlteracao;
    public decimal? TaxaEmbalagem { get; set; } = taxaEmbalagem;
    public decimal? TaxaInterna { get; set; } = taxaInterna;
    public decimal? OutrasDespesas { get; set; } = outrasDespesas;
    public decimal? Desconto { get; set; } = desconto;
    public decimal? Frete { get; set; } = frete;
    public string? AcordoFornecimento { get; set; } = acordoFornecimento;
    public decimal? ValorTotal { get; set; } = valorTotal;
    public EStatusOrdemDeCompra Status { get; set; } = status;
    public List<OrdemDeCompraProdutoDto>? Produtos { get; set; } = produtos;
    public List<OrdemDeCompraAnexoDto>? Anexos { get; set; } = anexos;
    public List<OrdemDeCompraObservacaoDto>? Observacoes { get; set; } = observacoes;
    public List<OrdemDeCompraUnidadeMedidaDto>? UnidadesMedida { get; set; } = unidadesMedida;
}

