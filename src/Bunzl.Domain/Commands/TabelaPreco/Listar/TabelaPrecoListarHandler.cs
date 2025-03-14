using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using Bunzl.Core.Domain.DTOs.DevExpress;

namespace Bunzl.Domain.Commands.TabelaPreco.Listar;

public class TabelaPrecoListarHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<TabelaPrecoListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(TabelaPrecoListarRequest request, CancellationToken cancellationToken)
    {
        var response = await repositoryTabelaPreco.ListDevExpressAsync<TabelaPrecoListarResponse>(false, request, p => p.FornecedorId == request.FornecedorId && p.EmpresaId == usuarioAutenticado.UsuarioEmpresa);
        return new CommandResponse<DataSourcePageResponse>(response, this);
    }
}
