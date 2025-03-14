using System.Text.Json.Serialization;

namespace Bunzl.Core.Domain.DTOs.Gateway;

public class GatewayOrdemDeCompraDto
{
    [JsonPropertyName("codigoFornecedor")]
    public string? CodigoFornecedor { get; set; }

    [JsonPropertyName("codigoERPFornecedor")]
    public string? CodigoErpFornecedor { get; set; }

    [JsonPropertyName("paisImportador")]
    public string? PaisImportador { get; set; }

    [JsonPropertyName("Empresa")]
    public string? Empresa { get; set; }

    [JsonPropertyName("numeroOrdem")]
    public string? NumeroOrdem { get; set; }

    [JsonPropertyName("dataOrdem")]
    public DateTime? DataOrdem { get; set; }

    [JsonPropertyName("numeroRevisao")]
    public int? NumeroRevisao { get; set; }

    [JsonPropertyName("dataRevisao")]
    public DateTime? DataRevisao { get; set; }

    [JsonPropertyName("codigoEstabelecimento")]
    public string? CodigoEstabelecimento { get; set; }

    [JsonPropertyName("dataExp")]
    public DateTime? DataExp { get; set; }

    [JsonPropertyName("nomeFornecedor")]
    public string? NomeFornecedor { get; set; }

    [JsonPropertyName("enderecoFornecedor")]
    public string? EnderecoFornecedor { get; set; }

    [JsonPropertyName("contatoFornecedor")]
    public string? ContatoFornecedor { get; set; }

    [JsonPropertyName("emailFornecedor")]
    public string? EmailFornecedor { get; set; }

    [JsonPropertyName("numeroEnderecoFornecedor")]
    public string? NumeroEnderecoFornecedor { get; set; }

    [JsonPropertyName("codigoFabricante")]
    public string? CodigoFabricante { get; set; }

    [JsonPropertyName("nomeFabricante")]
    public string? NomeFabricante { get; set; }

    [JsonPropertyName("enderecoFabricante")]
    public string? EnderecoFabricante { get; set; }

    [JsonPropertyName("nomeImportador")]
    public string? NomeImportador { get; set; }

    [JsonPropertyName("enderecoImportador")]
    public string? EnderecoImportador { get; set; }

    [JsonPropertyName("contatoImportador")]
    public string? ContatoImportador { get; set; }

    [JsonPropertyName("emailImportador")]
    public string? EmailImportador { get; set; }

    [JsonPropertyName("numeroEnderecoImportador")]
    public string? NumeroEnderecoImportador { get; set; }

    [JsonPropertyName("complementoEnderecoImportador")]
    public string? ComplementoEnderecoImportador { get; set; }

    [JsonPropertyName("bairroEnderecoImportador")]
    public string? BairroEnderecoImportador { get; set; }

    [JsonPropertyName("estadoProvinciaImportador")]
    public string? EstadoProvinciaImportador { get; set; }

    [JsonPropertyName("zipCodeImportador")]
    public string? ZipCodeImportador { get; set; }

    [JsonPropertyName("cnpjImportador")]
    public string? CnpjImportador { get; set; }

    [JsonPropertyName("prazoPagamento")]
    public string? PrazoPagamento { get; set; }

    [JsonPropertyName("tipoFrete")]
    public string? TipoFrete { get; set; }

    [JsonPropertyName("modoEntrega")]
    public string? ModoEntrega { get; set; }

    [JsonPropertyName("nomeAgente")]
    public string? NomeAgente { get; set; }

    [JsonPropertyName("destino")]
    public string? Destino { get; set; }

    [JsonPropertyName("numeroContainer20")]
    public int? NumeroContainer20 { get; set; }

    [JsonPropertyName("numeroContainer40")]
    public int? NumeroContainer40 { get; set; }

    [JsonPropertyName("numeroContainer40HC")]
    public int? NumeroContainer40HC { get; set; }

    [JsonPropertyName("numeroContainerOutros")]
    public int? NumeroContainerOutros { get; set; }

    [JsonPropertyName("totalCBM")]
    public decimal? TotalCBM { get; set; }

    [JsonPropertyName("pesoTotal")]
    public decimal? PesoTotal { get; set; }

    [JsonPropertyName("nomeComprador")]
    public string? NomeComprador { get; set; }

    [JsonPropertyName("nomeVendedor")]
    public string? NomeVendedor { get; set; }

    [JsonPropertyName("dataAlteracao")]
    public DateTime? DataAlteracao { get; set; }

    [JsonPropertyName("packingCharges")]
    public decimal? TaxaEmbalagem { get; set; }

    [JsonPropertyName("inlandCharges")]
    public decimal? TaxaInterna { get; set; }

    [JsonPropertyName("outrasDespesas")]
    public decimal? OutrasDespesas { get; set; }
    
    [JsonPropertyName("discount")]
    public decimal? Desconto { get; set; }

    [JsonPropertyName("freight")]
    public decimal? Frete { get; set; }

    [JsonPropertyName("observacoes")]
    public string? AcordoFornecimento { get; set; }

    [JsonPropertyName("produtos")]
    public List<GatewayProdutoOrdemDeCompraDto> Produtos { get; set; }
}


/*

 
   
   Download PI - Problemas (Não faz)
   
 
 
 
 */