using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Domain.Commands.Auditoria.Listar;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Application.Services;

public class AuditoriaAppService(IUnitOfWork unitOfWork, ISender mediator)
    : Notifiable, IAuditoriaAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> ListarAuditoria(AuditoriaListarRequest auditoriaListarDevExpressRequest)
    {
        var commandResponse = await mediator.Send(auditoriaListarDevExpressRequest);
        return commandResponse;
    }
}
