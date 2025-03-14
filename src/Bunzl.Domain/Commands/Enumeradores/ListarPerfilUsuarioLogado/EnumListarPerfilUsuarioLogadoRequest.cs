using Bunzl.Core.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Enumeradores.ListarPerfilUsuarioLogado;

public class EnumListarPerfilUsuarioLogadoRequest : IRequest<CommandResponse<IEnumerable<EnumDto>>>
{
}