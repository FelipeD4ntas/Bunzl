using Bunzl.Application.Interfaces;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.UoW;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Commands.Moeda.Adicionar;
using Bunzl.Domain.Commands.Moeda.Listar;

namespace Bunzl.Application.Services;

public class MoedaAppService(
	IUnitOfWork unitOfWork, 
	ISender mediator, 
	IRepositoryMoeda repositoryMoeda) : Notifiable, IMoedaAppService, IInjectScoped
{
    public async Task<CommandResponse<DataSourcePageResponse>> Listar(MoedaListarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        return commandResponse;
    }

    public async Task<CommandResponse<MoedaAdicionarResponse>> Adicionar(MoedaAdicionarRequest request)
    {
        var commandResponse = await mediator.Send(request);
        AddNotifications(commandResponse.Notificacoes);

        if (IsValid())
            await unitOfWork.CommitAsync();

        return commandResponse;
    }
}