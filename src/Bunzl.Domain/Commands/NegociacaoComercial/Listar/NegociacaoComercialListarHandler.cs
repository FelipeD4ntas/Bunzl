using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;
using System.Linq.Expressions;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Listar;

public class NegociacaoComercialListarHandler(
	IRepositoryNegociacaoComercial repositoryNegociacaoComercial,
	IRepositoryUsuario repositoryUsuario,
	IRepositoryFornecedor repositoryFornecedor,
	IUsuarioAutenticado usuarioAutenticado)
	: Notifiable, IRequestHandler<NegociacaoComercialListarRequest, CommandResponse<DataSourcePageResponse>>
{
	public async Task<CommandResponse<DataSourcePageResponse>> Handle(NegociacaoComercialListarRequest request, CancellationToken cancellationToken)
	{
        var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();

        Expression<Func<Entities.NegociacaoComercial, bool>> filtro;

		if (perfilUsuarioAtual == EPerfilUsuario.FornecedorEndUser)
		{
			var usuario = await repositoryUsuario.GetByAsync(false, u => u.Id == usuarioAutenticado.UsuarioId, cancellationToken, u => u.Fornecedores);
			if (usuario is null)
			{
				AddNotification("Usuario", UsuarioResources.UsuarioNaoEncontrado);
				return new CommandResponse<DataSourcePageResponse>(this);
			}

			var fornecedor = usuario.Fornecedores.FirstOrDefault();
			if (fornecedor is null)
			{
				AddNotification("Fornecedor", UsuarioResources.UsuarioNaoTemFornecedorAssociado);
				return new CommandResponse<DataSourcePageResponse>(this);
			}

			filtro = nc => nc.FornecedorId == fornecedor.Id && nc.EmpresaId == usuarioAutenticado.UsuarioEmpresa;
		}
		else
			filtro = nc => nc.EmpresaId == usuarioAutenticado.UsuarioEmpresa;

        var dataSourcePageResponse = await repositoryNegociacaoComercial
            .ListDevExpressAsync<NegociacaoComercialListarResponse>(
                false,
                request,
                filtro,
                nc => nc.Fornecedor);

		return new CommandResponse<DataSourcePageResponse>(dataSourcePageResponse, this);
	}
}