using System.Linq.Expressions;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;

namespace Bunzl.Domain.Commands.OrdemDeCompra.Listar;

public class OrdemDeCompraListarHandler(IRepositoryOrdemDeCompra repositoryOdemDeCompra, IUsuarioAutenticado usuarioAutenticado, IRepositoryUsuario repositoryUsuario)
    : Notifiable, IRequestHandler<OrdemDeCompraListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(OrdemDeCompraListarRequest request, CancellationToken cancellationToken)
    {
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        var empresaUsuarioAtual = usuarioAutenticado.UsuarioEmpresa;

        Expression<Func<Entities.OrdemDeCompra, bool>> filtro;

        if (perfilUsuarioAtual == EPerfilUsuario.FornecedorEndUser)
        {
            var usuario = await repositoryUsuario.GetByAsync(
                false,
                u => u.Id == usuarioAutenticado.UsuarioId,
                cancellationToken,
                u => u.Fornecedores);

            if (usuario is null)
            {
                AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
                return new CommandResponse<DataSourcePageResponse>(null, this);
            }

            var fornecedor = usuario.Fornecedores.FirstOrDefault();

            if (fornecedor is null)
            {
                AddNotification("Fornecedor", UsuarioResources.UsuarioNaoTemFornecedorAssociado);
                return new CommandResponse<DataSourcePageResponse>(null, this);
            }

            filtro = o => o.FornecedorId == fornecedor.Id && o.EmpresaId == empresaUsuarioAtual;
        }
        else
        {
            filtro = o => o.EmpresaId == empresaUsuarioAtual;
        }

        var dataSoucePageResponse = await repositoryOdemDeCompra
            .ListDevExpressAsync<OrdemDeCompraListarResponse>(
                false,
                request,
                filtro,
                nc => nc.Produtos,
                nc => nc.Anexos,
                nc => nc.UnidadesMedida);

        return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }
}

