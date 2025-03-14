using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarObservacao;

public class NegociacaoComercialAdicionarObservacaoHandler(
	IPublisher mediator, 
	IRepositoryNegociacaoComercial repositoryNegociacaoComercial,
	IRepositoryEmpresa repositoryEmpresa,
	IRepositoryFornecedor repositoryFornecedor,
	IRepositoryUsuario repositoryUsuario,
	IUsuarioAutenticado usuarioAutenticado)
	: Notifiable, IRequestHandler<NegociacaoComercialAdicionarObservacaoRequest, CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>>
{
	public async Task<CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>> Handle(NegociacaoComercialAdicionarObservacaoRequest request, CancellationToken cancellationToken)
	{
		var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, f => f.Id == request.NegociacaoComercialId, cancellationToken, p => p.NegociacaoComercialObservacoes);
		if (negociacaoComercial == null)
		{
			AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
			return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(this);
		}

		var NegociacaoComercialObservacao = new Entities.NegociacaoComercialObservacao(request.Observacao, negociacaoComercial.Id);
		negociacaoComercial.AdicionarObservacao(NegociacaoComercialObservacao);

		if (IsInvalid())
			return await Task.FromResult(new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(this));

		repositoryNegociacaoComercial.Update(negociacaoComercial);
		await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Observação Adicionada", ETipoAuditoria.Modificado));

		var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == negociacaoComercial.FornecedorId, cancellationToken, p => p.Usuarios);
		if (fornecedor == null)
		{
			AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
			return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(this);
		}

		var usuario = await repositoryUsuario.GetByAsync(true, u => u.Id == usuarioAutenticado.UsuarioId, cancellationToken, u => u.Empresas);
        if (usuario == null)
        {
            AddNotification("Fornecedor", UsuarioResources.UsuarioNaoEncontrado);
            return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(this);
        }

		if (usuarioAutenticado.Permissoes == EPerfilUsuario.FornecedorEndUser.ToString())
		{
			var empresa = await repositoryEmpresa.GetByAsync(true, u => u.Id == negociacaoComercial.EmpresaId, false, cancellationToken, e => e.Usuarios);
			if (empresa == null)
			{
				AddNotification("Empresa", EmpresaResources.EmpresaNaoEncontrada);
				return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(this);
			}

			var usuariosEmpresaEmail = empresa.Usuarios.Where(u => u.PerfilPermissao != EPerfilUsuario.FornecedorEndUser).ToList();

			return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(
				new NegociacaoComercialAdicionarObservacaoResponse(negociacaoComercial.Id, FornecedorResources.ObservacaoAdicionadaComSucesso, fornecedor, negociacaoComercial.Codigo.ToString(), usuario.Nome, usuariosEmpresaEmail),
				this);
		}
		else
		{
			return new CommandResponse<NegociacaoComercialAdicionarObservacaoResponse>(
				new NegociacaoComercialAdicionarObservacaoResponse(negociacaoComercial.Id, FornecedorResources.ObservacaoAdicionadaComSucesso, fornecedor, negociacaoComercial.Codigo.ToString(), usuario.Nome),
				this);
		}
	}
}

