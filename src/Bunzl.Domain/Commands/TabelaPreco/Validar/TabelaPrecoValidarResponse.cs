using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.TabelaPreco.Validar;

public class TabelaPrecoValidarResponse(Guid id, string mensagem, Entities.Fornecedor fornecedor, bool tabelaValidada) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;

    [JsonIgnore]
    public bool TabelaValidada { get; set; } = tabelaValidada;
}
