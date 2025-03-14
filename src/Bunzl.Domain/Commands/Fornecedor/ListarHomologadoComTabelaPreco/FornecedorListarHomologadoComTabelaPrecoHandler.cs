using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarHomologadoComTabelaPreco;

public class FornecedorListarHomologadoComTabelaPrecoHandler(IRepositoryFornecedor repositoryFornecedor, IUsuarioAutenticado usuarioAutenticado) 
    : Notifiable, IRequestHandler<FornecedorListarHomologadoComTabelaPrecoRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorListarHomologadoComTabelaPrecoRequest request, CancellationToken cancellationToken)
    {
        var dataSoucePageResponse = await repositoryFornecedor.ListDevExpressAsync<FornecedorListarHomologadoComTabelaPrecoResponse>(false, request,
            p => p.Status == EStatusFornecedor.Homologado && 
                 p.Empresas.Any(fe => fe.Id == usuarioAutenticado.UsuarioEmpresa) &&
                 p.TabelasPreco.Any(tp => tp.Status == EStatusTabelaPreco.Integrada && tp.FlagExpirada == false),
            p => p.TabelasPreco);

        return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }
}
