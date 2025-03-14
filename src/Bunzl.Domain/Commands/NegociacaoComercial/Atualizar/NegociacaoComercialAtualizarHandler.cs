using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;

public class NegociacaoComercialAtualizarHandler(
	IPublisher mediator,
	IRepositoryNegociacaoComercial repositoryNegociacaoComercial)
	: Notifiable, IRequestHandler<NegociacaoComercialAtualizarRequest, CommandResponse<NegociacaoComercialAtualizarResponse>>
{
	public async Task<CommandResponse<NegociacaoComercialAtualizarResponse>> Handle(NegociacaoComercialAtualizarRequest request, CancellationToken cancellationToken)
	{
		var negociacaoComercial = await repositoryNegociacaoComercial.GetByAsync(true, f => f.Id == request.Id, cancellationToken, p => p.NegociacaoComercialObservacoes, p => p.Produtos, p => p.Anexos);
		if (negociacaoComercial == null)
		{
			AddNotification("NegociacaoComercial", NegociacaoComercialResources.NegociacaoComercialNaoEncontrada);
			return new CommandResponse<NegociacaoComercialAtualizarResponse>(this);
		}

		negociacaoComercial.Atualizar(request.Titulo, request.DataEntrega, request.CampoAtuacao, request.TermosPagamento, request.ValorTotal);

		if (request.Produtos != null)
		{
			negociacaoComercial.AtualizarProdutos(request.Produtos);
		}

        repositoryNegociacaoComercial.Update(negociacaoComercial);
        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.NegociacaoComercial, "Atualizado", ETipoAuditoria.Modificado), cancellationToken);

        return new CommandResponse<NegociacaoComercialAtualizarResponse>(new NegociacaoComercialAtualizarResponse(negociacaoComercial.Id, NegociacaoComercialResources.NegociacaoComercialAtualizadaComSucesso), this);
    }
}

