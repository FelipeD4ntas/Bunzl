using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Infra.CrossCutting.Security.Autenticacao;
using MediatR;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;

public class OrdemDeCompraDeletarObservacaoHandler(IPublisher mediator, IRepositoryOrdemDeCompra repositoryOrdemDeCompra, IUsuarioAutenticado usuarioAutenticado)
    : Notifiable, IRequestHandler<OrdemDeCompraDeletarObservacaoRequest, CommandResponse<OrdemDeCompraDeletarObservacaoResponse>>
{
    public async Task<CommandResponse<OrdemDeCompraDeletarObservacaoResponse>> Handle(OrdemDeCompraDeletarObservacaoRequest request, CancellationToken cancellationToken)
    {
        var negociacaoComercial = await repositoryOrdemDeCompra.GetByAsync(true, c => c.Id == request.OrdemDeCompraId, cancellationToken, p => p.Observacoes);
        if (negociacaoComercial == null)
        {
            AddNotification("OrdemDeCompra", OrdemDeCompraResources.OrdemDeCompraNaoEncontrada);
            return new CommandResponse<OrdemDeCompraDeletarObservacaoResponse>(this);
        }

        var observacao = negociacaoComercial.Observacoes.FirstOrDefault(x => x.Id == request.ObservacaoId);
        if (observacao == null)
        {
            AddNotification("Observacao", OrdemDeCompraResources.ObservacaoNaoEncontrada);
            return new CommandResponse<OrdemDeCompraDeletarObservacaoResponse>(this);
        }

        negociacaoComercial.DeletarObservacao(observacao);
        repositoryOrdemDeCompra.Update(negociacaoComercial);
        await mediator.Publish(new AuditoriaAdicionarInput(negociacaoComercial.Id, TabelasResources.Fornecedor, "Observação Deletada", ETipoAuditoria.Modificado));

        var response = new OrdemDeCompraDeletarObservacaoResponse(observacao.Id, OrdemDeCompraResources.ObservacaoDeletadaComSucesso);
        return new CommandResponse<OrdemDeCompraDeletarObservacaoResponse>(response, this);
    }
}
