using Bunzl.Domain.Enumerators;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;

public class FornecedorProdutoAdicionarAnexoPayload
{
    public Guid? FornecedorId { get; set; } 
    public Guid? FornecedorProdutoId { get; set; }
    public required IFormFile Arquivo { get; set; } 
    public string? Observacao { get; set; }
    public ETipoDocumento TipoDocumento { get; set; }

    public FornecedorProdutoAdicionarAnexoRequest ToRequest(Guid fornecedorId, Guid fornecedorProdutoId)
    {
        return new FornecedorProdutoAdicionarAnexoRequest(fornecedorId, fornecedorProdutoId, Arquivo, Observacao, TipoDocumento);
    }
}
