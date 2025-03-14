using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarDocumentos;

public class FornecedorListarDocumentosResponse
{
    public IEnumerable<FornecedorDocumentoDto>? FornecedorDocumentos { get; set; }
}
