using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class OrdemDeCompra : EntityBase<Guid>, IAggregationRoot
{
    public Guid FornecedorId { get; set; }
    public Guid EmpresaId { get; set; }
    public string? CodigoFornecedor { get; set; }
    public string? CodigoErpFornecedor { get; set; }
    public string? PaisImportador { get; set; }
    public string? NumeroOrdem { get; set; }
    public DateTime? DataOrdem { get; set; }
    public string? NumeroRevisao { get; set; }
    public DateTime? DataRevisao { get; set; }
    public string? CodigoEstabelecimento { get; set; }
    public DateTime? DataExp { get; set; }
    public string? NomeFornecedor { get; set; }
    public string? EnderecoFornecedor { get; set; }
    public string? NumeroEnderecoFornecedor { get; set; }
    public string? ContatoFornecedor { get; set; }
    public string? EmailFornecedor { get; set; }
    public string? CodigoFabricante { get; set; }
    public string? NomeFabricante { get; set; }
    public string? EnderecoFabricante { get; set; }
    public string? NomeImportador { get; set; }
    public string? EnderecoImportador { get; set; }
    public string? ContatoImportador { get; set; }
    public string? EmailImportador { get; set; }
    public string? NumeroEnderecoImportador { get; set; }
    public string? ComplementoEnderecoImportador { get; set; }
    public string? BairroEnderecoImportador { get; set; }
    public string? EstadoProvinciaImportador { get; set; }
    public string? ZipCodeImportador { get; set; }
    public string? CnpjImportador { get; set; }
    public string? PrazoPagamento { get; set; }
    public string? TipoFrete { get; set; }
    public string? ModoEntrega { get; set; }
    public string? NomeAgente { get; set; }
    public string? Destino { get; set; }
    public int? NumeroContainer20 { get; set; }
    public int? NumeroContainer40 { get; set; }
    public int? NumeroContainer40HC { get; set; }
    public int? NumeroContainerOutros { get; set; }
    public decimal? TotalCBM { get; set; }
    public decimal? PesoTotal { get; set; }
    public string? NomeComprador { get; set; }
    public string? NomeVendedor { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public decimal? TaxaEmbalagem { get; set; }
    public decimal? TaxaInterna { get; set; }
    public decimal? OutrasDespesas { get; set; }
    public decimal? Desconto { get; set; }
    public decimal? Frete { get; set; }
    public string? AcordoFornecimento { get; set; }
    public decimal? ValorTotal { get; set; }
	public EStatusOrdemDeCompra Status { get; set; } = EStatusOrdemDeCompra.AguardandoPi;
    public virtual List<OrdemDeCompraProduto> Produtos { get; set; } = [];
    public virtual List<OrdemDeCompraAnexo> Anexos { get; set; } = [];
    public virtual List<OrdemDeCompraObservacao> Observacoes { get; set; } = [];
    public virtual List<OrdemDeCompraUnidadeMedida> UnidadesMedida { get; set; } = [];
    public virtual Fornecedor Fornecedor { get; set; }

    public void AdicionarProduto(OrdemDeCompraProduto produto)
    {
        Produtos.Add(produto);
    }

    public void AdicionarAnexo(OrdemDeCompraAnexo anexo)
    {
        Anexos.Add(anexo);
    }

    public void AdicionarObservacao(OrdemDeCompraObservacao observacao)
    {
        Observacoes.Add(observacao);
    }

    public void DeletarAnexo(OrdemDeCompraAnexo anexo)
    {
        Anexos.Remove(anexo);
    }

    public void DeletarObservacao(OrdemDeCompraObservacao observacao)
    {
        Observacoes.Remove(observacao);
    }

    public void AtualizarStatus(EStatusOrdemDeCompra status)
    {
	    Status = status;
    }
}