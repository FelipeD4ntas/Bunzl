using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarObservacao;

public class FornecedorDeletarObservacaoHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorDeletarObservacaoRequest, CommandResponse<FornecedorDeletarObservacaoResponse>>
{
    public async Task<CommandResponse<FornecedorDeletarObservacaoResponse>> Handle(FornecedorDeletarObservacaoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == request.FornecedorId, cancellationToken, p => p.FornecedorObservacoes);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorDeletarObservacaoResponse>(this);
        }

        var observacao = fornecedor.FornecedorObservacoes.Where(x => x.Id == request.ObservacaoId).FirstOrDefault();
        if (observacao == null)
        {
            AddNotification("Observacao", FornecedorResources.ObservacaoNaoEncontrada);
            return new CommandResponse<FornecedorDeletarObservacaoResponse>(this);
        }

        fornecedor.DeletarObservacao(observacao);
        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Observação Deletada", ETipoAuditoria.Modificado));

        var response = new FornecedorDeletarObservacaoResponse(observacao.Id, FornecedorResources.ObservacaoDeletadaComSucesso);
        return new CommandResponse<FornecedorDeletarObservacaoResponse>(response, this);
    }
}
