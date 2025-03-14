using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;
using prmToolkit.EnumExtension;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AtualizarStatus;

public class OrdemDeCompraAtualizarStatusHandler(
	IPublisher mediator,
	IRepositoryOrdemDeCompra repositoryOrdemDeCompra,
	IUsuarioAutenticado usuarioAutenticado)
	: Notifiable, IRequestHandler<OrdemDeCompraAtualizarStatusRequest, CommandResponse<OrdemDeCompraAtualizarStatusResponse>>
{
	public async Task<CommandResponse<OrdemDeCompraAtualizarStatusResponse>> Handle(OrdemDeCompraAtualizarStatusRequest request, CancellationToken cancellationToken)
	{
		var perfilUsuarioAtual = usuarioAutenticado.Permissoes.ToEnum<EPerfilUsuario>();
		if (perfilUsuarioAtual is EPerfilUsuario.FornecedorEndUser or EPerfilUsuario.BunzlCorporativoMasterUser)
		{
			AddNotification("Usuario", OrdemDeCompraResources.UsuarioNaoPodeAlterarStatusDaOrdemDeCompra);
			return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(this);
		}

		var ordemDeCompra = await repositoryOrdemDeCompra.GetByAsync(true, p => p.Id == request.Id && p.EmpresaId == usuarioAutenticado.UsuarioEmpresa, cancellationToken, p => p.Fornecedor);
		if (ordemDeCompra is null)
		{
			AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
			return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(this);
		}

        if (ordemDeCompra.Status is EStatusOrdemDeCompra.EmProducao && request.Status != EStatusOrdemDeCompra.Finalizada)
        {
			AddNotification("Status", OrdemDeCompraResources.UsuarioNaoPodeAlterarParaEsseStatus);
            return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(this);
        }

        if (ordemDeCompra.Status == EStatusOrdemDeCompra.Cancelada && request.Status != EStatusOrdemDeCompra.AguardandoPi)
        {
            AddNotification("Status", OrdemDeCompraResources.OrdemDeCompraSoPodeSerAtualizadaParaAguardandoPi);
            return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(this);
        }

        ordemDeCompra.AtualizarStatus(request.Status);
		repositoryOrdemDeCompra.Update(ordemDeCompra);
		
        if (IsInvalid())
			return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(this);

		await mediator.Publish(new AuditoriaAdicionarInput(ordemDeCompra.Id, TabelasResources.OrdemCompra, "Status Atualizado", Enumerators.ETipoAuditoria.Modificado), cancellationToken);

		return new CommandResponse<OrdemDeCompraAtualizarStatusResponse>(
			new OrdemDeCompraAtualizarStatusResponse(ordemDeCompra.Id, OrdemDeCompraResources.StatusAtualizadoComSucesso, ordemDeCompra.Fornecedor, ordemDeCompra.Status.GetDescriptionLanguage(usuarioAutenticado.Idioma), ordemDeCompra.NumeroOrdem), this);
	}
}

