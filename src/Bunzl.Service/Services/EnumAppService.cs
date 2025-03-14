using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.Enumerators;
using Bunzl.Domain.Commands.Enumeradores.ListarPerfilUsuarioLogado;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Application.Services;

public class EnumAppService(ISender mediator) : Notifiable, IEnumAppService, IInjectScoped
{
    public async Task<CommandResponse<IEnumerable<EnumDto>>> ListarPerfilUsuarioLogado(EnumListarPerfilUsuarioLogadoRequest request)
    {
        return await mediator.Send(request);
    }
}