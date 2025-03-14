using Bunzl.Core.Domain.Enumerators;
using Bunzl.Domain.Commands.Enumeradores.ListarPerfilUsuarioLogado;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface IEnumAppService
{
    Task<CommandResponse<IEnumerable<EnumDto>>> ListarPerfilUsuarioLogado(EnumListarPerfilUsuarioLogadoRequest request);
}