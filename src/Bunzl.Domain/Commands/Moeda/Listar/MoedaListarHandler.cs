using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Domain.Commands.Moeda.Listar;

public class MoedaListarHandler(
    IRepositoryMoeda repositoryMoeda) : Notifiable, IRequestHandler<MoedaListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(MoedaListarRequest request, CancellationToken cancellationToken)
    {
		var dataSoucePageResponse = await repositoryMoeda
				.ListDevExpressAsync<MoedaListarResponse>(
					false,
					request);

		return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }
}
