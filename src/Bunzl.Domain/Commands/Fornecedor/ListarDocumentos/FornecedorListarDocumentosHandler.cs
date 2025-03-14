using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarDocumentos;

public class FornecedorListarDocumentosHandler(IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorListarDocumentosRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorListarDocumentosRequest request, CancellationToken cancellationToken)
    {
        var responseFornecedoresList = await repositoryFornecedor
            .ListDevExpressFornecedorDocumentoAsync<FornecedorDocumentoDto>(
                false,
                request,
                request.FornecedorId);

        return new CommandResponse<DataSourcePageResponse>(responseFornecedoresList, this);
    }
}