using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;

public class FornecedorAdicionarDocumentoPayload
{
    public required IFormFile Arquivo { get; set; }
    public string? Observacao { get; set; }

    public FornecedorAdicionarDocumentoRequest ToRequest(Guid fornecedorId)
    {
        return new FornecedorAdicionarDocumentoRequest(fornecedorId, Arquivo, Observacao);
    }
}
