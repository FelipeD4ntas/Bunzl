using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ListarAnexos;

public class OrdemDeCompraListarAnexosHandler(IRepositoryOrdemDeCompra repositoryOrdemDeCompra)
    : Notifiable, IRequestHandler<OrdemDeCompraListarAnexosRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(OrdemDeCompraListarAnexosRequest request, CancellationToken cancellationToken)
    {
        var responseNegociacoes = await repositoryOrdemDeCompra
            .ListDevExpressOrdemDeCompraAnexoAsync<OrdemDeCompraAnexoListarDto>(
                false,
                request,
                request.OrdemDeCompraId);

        return new CommandResponse<DataSourcePageResponse>(responseNegociacoes, this);
    }
}