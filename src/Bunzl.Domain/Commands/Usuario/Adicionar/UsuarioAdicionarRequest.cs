using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Adicionar;

public class UsuarioAdicionarRequest : IRequest<CommandResponse<UsuarioAdicionarResponse>>
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required EPerfilUsuario PerfilPermissao { get; set; }
    public required IEnumerable<Guid> EmpresasId { get; set; }
}