using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;

public class FornecedorAdicionarObservacaoHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorAdicionarObservacaoRequest, CommandResponse<FornecedorAdicionarObservacaoResponse>>
{
    public async Task<CommandResponse<FornecedorAdicionarObservacaoResponse>> Handle(FornecedorAdicionarObservacaoRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, f => f.Id == request.FornecedorId, cancellationToken, p => p.FornecedorObservacoes);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorAdicionarObservacaoResponse>(this);
        }

        var fornecedorObservacao = new Entities.FornecedorObservacao
        {
            Observacao = request.Observacao,
            FornecedorId = fornecedor.Id,
            Fornecedor = fornecedor
        };
        fornecedor.AdicionarObservacao(fornecedorObservacao);

        if (IsInvalid())
            return await Task.FromResult(new CommandResponse<FornecedorAdicionarObservacaoResponse>(this));

        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.Fornecedor, "Observação Adicionada", ETipoAuditoria.Modificado));

        return new CommandResponse<FornecedorAdicionarObservacaoResponse>(
            new FornecedorAdicionarObservacaoResponse(fornecedor.Id, FornecedorResources.ObservacaoAdicionadaComSucesso),
            this);
    }
}