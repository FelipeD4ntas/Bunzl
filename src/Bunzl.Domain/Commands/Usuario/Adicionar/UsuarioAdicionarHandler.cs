using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Interfaces.Services;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Adicionar;

public class UsuarioAdicionarHandler(
    IPublisher mediator, 
    IRepositoryUsuario repositoryUsuario, 
    IRepositoryEmpresa repositoryEmpresa, 
    IUsuarioAutenticado usuarioAutenticado) : Notifiable, IRequestHandler<UsuarioAdicionarRequest, CommandResponse<UsuarioAdicionarResponse>>
{
    public async Task<CommandResponse<UsuarioAdicionarResponse>> Handle(UsuarioAdicionarRequest request, CancellationToken cancellationToken)
    {
        var perfilSolicitantePodeCadastrar = PerfilSolicitantePodeCadastrar(request);
        if (!perfilSolicitantePodeCadastrar)
        {
            AddNotification("EPerfilUsuario", UsuarioResources.PerfilInvalido);
            return await Task.FromResult(new CommandResponse<UsuarioAdicionarResponse>(this));
        }

        var empresas = await repositoryEmpresa.ListAsync(true, 0, 100, e => request.EmpresasId.Contains(e.Id));
        if (empresas is null)
        {
            AddNotification("Empresas", UsuarioResources.UsuarioFalhaRelacionarEmpresas);
            return new CommandResponse<UsuarioAdicionarResponse>(this);
        }

        var usuario = await repositoryUsuario.GetByAsync(true, u => u.Email == request.Email, true, cancellationToken, u => u.Empresas, u => u.Fornecedores);

        if (usuario is not null)
		{
            //Associar o Usuário a todas as empresa que veio no request
            var houveAssociacao = false;
			request.EmpresasId.ForEach(empresaId =>
			{
                if (usuario.Empresas.All(e => e.Id != empresaId))
				{
					var empresa = empresas.FirstOrDefault(e => e.Id == empresaId);
					if (empresa is not null)
					{
						houveAssociacao = true;
						usuario.RelacionarEmpresa(empresa);
					}
				}
			});

			if (!houveAssociacao)
			{
				AddNotification("Email", UsuarioResources.UsuarioExistenteJaRelacionadoComEssaEmpresa);
				return new CommandResponse<UsuarioAdicionarResponse>(this);
			}

			repositoryUsuario.Update(usuario);

			return new CommandResponse<UsuarioAdicionarResponse>(
				new UsuarioAdicionarResponse(
					usuario.Id,
					UsuarioResources.UsuarioAtualizadoComSucesso,
					usuario.Nome,
					usuario.Email,
					true,
					empresas.FirstOrDefault()!.Nome),
				this);
		}

		var usuarioNovo = new Entities.Usuario
		{
			Email = request.Email,
			Nome = request.Nome,
			PerfilPermissao = request.PerfilPermissao,
			FlagVeioDoERP = false
		};

		usuarioNovo.RelacionarEmpresas(empresas.ToList());
		usuarioNovo.GerarChaveCadastro();

		await repositoryUsuario.AddAsync(usuarioNovo, cancellationToken);

		await mediator.Publish(new AuditoriaAdicionarInput(usuarioNovo.Id, TabelasResources.Usuario, "Criado", Enumerators.ETipoAuditoria.Adicionado));

		return new CommandResponse<UsuarioAdicionarResponse>(
			new UsuarioAdicionarResponse(
				usuarioNovo.Id,
				UsuarioResources.UsuarioAdicionadoComSucesso,
				usuarioNovo.Nome,
				usuarioNovo.Email,
				usuarioNovo.ChaveCadastro!.Value),
			this);
	}

	private bool PerfilSolicitantePodeCadastrar(UsuarioAdicionarRequest request)
    {
        var perfilSolicitante = usuarioAutenticado.Permissoes;
        var empresaSolicitante = usuarioAutenticado.UsuarioEmpresa;

        if (perfilSolicitante != Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser.ToString() && empresaSolicitante != request.EmpresasId.FirstOrDefault())
            return false;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser.ToString() &&
            (request.PerfilPermissao == Enumerators.EPerfilUsuario.AdministradorSuperUser ||
            request.PerfilPermissao == Enumerators.EPerfilUsuario.CompradorKeyUser ||
            request.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser ||
            request.PerfilPermissao == Enumerators.EPerfilUsuario.BunzlCorporativoMasterUser))
            return true;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.AdministradorSuperUser.ToString() &&
            (request.PerfilPermissao == Enumerators.EPerfilUsuario.CompradorKeyUser ||
            request.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser))
            return true;

        if (perfilSolicitante == Enumerators.EPerfilUsuario.CompradorKeyUser.ToString() &&
            request.PerfilPermissao == Enumerators.EPerfilUsuario.FornecedorEndUser)
            return true;

        return false;
    }
}