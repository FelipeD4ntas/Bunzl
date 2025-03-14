using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ListarAnexos;

public class NegociacaoComercialListarAnexosHandler(IRepositoryNegociacaoComercial repositoryNegociacaoComercial)
    : Notifiable, IRequestHandler<NegociacaoComercialListarAnexosRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(NegociacaoComercialListarAnexosRequest request, CancellationToken cancellationToken)
    {
        var responseNegociacoes = await repositoryNegociacaoComercial
            .ListDevExpressNegociacaoComercialAnexoAsync<NegociacaoComercialAnexoListarDto>(
                false,
                request,
                request.NegociacaoComercialId);

        return new CommandResponse<DataSourcePageResponse>(responseNegociacoes, this);
    }
}