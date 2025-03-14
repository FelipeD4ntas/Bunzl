using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Domain.Commands.Incoterm.Listar;

public class IncotermListarHandler(
    IRepositoryIncoterm repositoryIncoterm) : Notifiable, IRequestHandler<IncotermListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(IncotermListarRequest request, CancellationToken cancellationToken)
    {
		var dataSoucePageResponse = await repositoryIncoterm
				.ListDevExpressAsync<IncotermListarResponse>(
					false,
					request);

		return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }
}
