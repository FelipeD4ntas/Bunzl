using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Usuario.Atualizar;

public class UsuarioAtualizarRequest : IRequest<CommandResponse<UsuarioAtualizarResponse>>
{
    [JsonIgnore]
    public Guid? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}