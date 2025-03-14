using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.ObterAnexoProduto;

public class FornecedorProdutoObterAnexoResponse(Guid id, string nome, string tipo, ETipoDocumento tipoDocumento, string observacao, DateTime dataCriacao, Guid fornecedorProdutoId, string? arquivoBase64 = null)
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Tipo { get; set; } = tipo;
    public ETipoDocumento TipoDocumento { get; set; } = tipoDocumento;
    public string? Observacao { get; set; } = observacao;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public Guid FornecedorProdutoId { get; set; } = fornecedorProdutoId;
    public string? ArquivoBase64 { get; set; } = arquivoBase64;
}
