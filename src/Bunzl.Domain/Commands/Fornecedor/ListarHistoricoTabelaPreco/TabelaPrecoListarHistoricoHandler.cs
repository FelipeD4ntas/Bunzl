using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarHistoricoTabelaPreco;

public class TabelaPrecoListarHistoricoHandler(
    IRepositoryFornecedor repositoryFornecedor, 
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<TabelaPrecoListarHistoricoRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(TabelaPrecoListarHistoricoRequest request, CancellationToken cancellationToken)
    {
        var dataSourcePageResponse = await repositoryFornecedor
            .ListDevExpressFornecedorTabelasPrecoAsync<TabelaPrecoHistoricoDto>(
                false,
                request,
                tp => tp.EmpresaId == usuarioAutenticado.UsuarioEmpresa && tp.FornecedorId == request.FornecedorId
                );

        return new CommandResponse<DataSourcePageResponse>(dataSourcePageResponse, this);
    }
}

