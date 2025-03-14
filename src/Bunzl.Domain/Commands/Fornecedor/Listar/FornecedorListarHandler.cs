using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;
using System.Linq.Expressions;

namespace Bunzl.Domain.Commands.Fornecedor.Listar;

public class FornecedorListarHandler(
    IRepositoryFornecedor repositoryFornecedor, 
    IUsuarioAutenticado usuarioAutenticado) 
    : Notifiable, IRequestHandler<FornecedorListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(FornecedorListarRequest request, CancellationToken cancellationToken)
    {
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        var empresaUsuarioAtual = usuarioAutenticado.UsuarioEmpresa;

        var filtroPerfil = ObterFiltroPorPerfil(perfilUsuarioAtual, empresaUsuarioAtual);

		var dataSoucePageResponse = await repositoryFornecedor
				.ListDevExpressAsync<FornecedorListarResponse>(
					false,
					request,
					filtroPerfil);

		return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }

    private static Expression<Func<Entities.Fornecedor, bool>> ObterFiltroPorPerfil(EPerfilUsuario perfilUsuarioLogado, Guid empresaUsuarioAtual)
    {
        return perfilUsuarioLogado switch
        {
            EPerfilUsuario.BunzlCorporativoMasterUser => f => 
                f.Empresas.Any(e => e.Id == empresaUsuarioAtual),
            EPerfilUsuario.AdministradorSuperUser => f =>
                f.Empresas.Any(e => e.Id == empresaUsuarioAtual),
            EPerfilUsuario.CompradorKeyUser => f =>
                f.Empresas.Any(e => e.Id == empresaUsuarioAtual),
            _ => f => false 
        };
    }
}
