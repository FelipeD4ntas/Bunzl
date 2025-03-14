using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterPorUsuario;

public class EmpresaObterPorUsuarioRequest(Guid usuarioId) : IRequest<CommandResponse<IEnumerable<EmpresaObterPorUsuarioResponse>>>
{
    public Guid UsuarioId { get; } = usuarioId;
}
