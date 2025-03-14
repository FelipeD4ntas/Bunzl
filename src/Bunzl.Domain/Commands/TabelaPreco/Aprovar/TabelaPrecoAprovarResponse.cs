using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.TabelaPreco.Aprovar;

public class TabelaPrecoAprovarResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;
}
