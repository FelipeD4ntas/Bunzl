using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;

public class UsuarioAtualizarSenhaTelefoneRequest : IRequest<CommandResponse<UsuarioAtualizarSenhaTelefoneResponse>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public required string Telefone { get; set; }
    public required string SenhaAtual { get; set; }
    public required string NovaSenha { get; set; }
    public required string ConfirmacaoNovaSenha { get; set; }
    public required string Area { get; set; }
}
