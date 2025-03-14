using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;
using System.Linq.Expressions;

namespace Bunzl.Domain.Commands.Usuario.Listar;

public class UsuarioListarHandler(IRepositoryUsuario repositoryUsuario, IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<UsuarioListarRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(UsuarioListarRequest request, CancellationToken cancellationToken)
    {
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
        var empresaUsuarioAtual = usuarioAutenticado.UsuarioEmpresa;

        var filtroPerfil = ObterFiltroPorPerfil(perfilUsuarioAtual);

        var dataSoucePageResponse = await repositoryUsuario
            .ListDevExpressAsync<UsuarioListarResponse>(
            false,
            request,
            filtroPerfil,
            x => x.Empresas);

        return new CommandResponse<DataSourcePageResponse>(dataSoucePageResponse, this);
    }

    public Expression<Func<Entities.Usuario, bool>> ObterFiltroPorPerfil(EPerfilUsuario perfilUsuarioLogado)
    {
        return perfilUsuarioLogado switch
        {
            EPerfilUsuario.BunzlCorporativoMasterUser => u =>
                u.PerfilPermissao == EPerfilUsuario.BunzlCorporativoMasterUser ||
                u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser ||
                u.PerfilPermissao == EPerfilUsuario.FornecedorEndUser,
            EPerfilUsuario.AdministradorSuperUser => u =>
                u.PerfilPermissao == EPerfilUsuario.CompradorKeyUser ||
                u.PerfilPermissao == EPerfilUsuario.FornecedorEndUser,
            EPerfilUsuario.CompradorKeyUser => u =>
                u.PerfilPermissao == EPerfilUsuario.FornecedorEndUser,
            _ => u => false
        };
    }
}