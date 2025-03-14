using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.Adicionar;

public class FornecedorAdicionarResponse(Guid id, string mensagem, List<Entities.Usuario>? usuarios = null, string? nomeFornecedor = null) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    [JsonIgnore]
    public string? NomeFornecedor { get; set; } = nomeFornecedor;
}