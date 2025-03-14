using Bunzl.Domain.DTOs.Base;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;

public class UsuarioSolicitarResetSenhaResponse(Guid id, string mensagem, string nome, string email, Guid chaveResetSenha) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public string Nome { get; set; } = nome;

    [JsonIgnore]
    public string Email { get; set; } = email;

    [JsonIgnore]
    public Guid ChaveResetSenha { get; set; } = chaveResetSenha;
}
