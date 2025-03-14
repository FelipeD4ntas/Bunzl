using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Commands.Incoterm.Listar;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Application.Services;

public class IncotermAppService(
	ISender mediator) : Notifiable, IIncotermAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> Listar(IncotermListarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }
}