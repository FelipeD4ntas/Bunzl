using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.Entities;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;

public class FornecedorAdicionarDocumentoResponse(Guid id, string mensagem, FornecedorDocumento fornecedorDocumento) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public FornecedorDocumento FornecedorDocumento { get; set; } = fornecedorDocumento;
    public Guid DocumentoId { get; set; }
}