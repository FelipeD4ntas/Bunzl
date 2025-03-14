using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Domain.Notifications.Auditoria.Adicionar;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarProduto;

public class FornecedorProdutoDeletarHandler(IPublisher mediator, IRepositoryFornecedor repositoryFornecedor)
    : Notifiable, IRequestHandler<FornecedorProdutoDeletarRequest, CommandResponse<FornecedorProdutoDeletarResponse>>
{
    public async Task<CommandResponse<FornecedorProdutoDeletarResponse>> Handle(FornecedorProdutoDeletarRequest request, CancellationToken cancellationToken)
    {
        var fornecedor = await repositoryFornecedor.GetByAsync(true, c => c.Id == request.FornecedorId, cancellationToken, p => p.FornecedorProdutos);
        if (fornecedor == null)
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorNaoEncontrado);
            return new CommandResponse<FornecedorProdutoDeletarResponse>(this);
        }

        var produto = fornecedor.FornecedorProdutos.Where(x => x.Id == request.ProdutoId).FirstOrDefault();
        if (produto == null)
        {
            AddNotification("Produto", FornecedorResources.ProdutoNaoEncontrado);
            return new CommandResponse<FornecedorProdutoDeletarResponse>(this);
        }

        fornecedor.DeletarProduto(produto);
        repositoryFornecedor.Update(fornecedor);
        await mediator.Publish(new AuditoriaAdicionarInput(fornecedor.Id, TabelasResources.FornecedorProduto, "Produto Deletado", ETipoAuditoria.Modificado), cancellationToken);

        var response = new FornecedorProdutoDeletarResponse(produto.Id, FornecedorResources.ProdutoDeletadoComSucesso);
        return new CommandResponse<FornecedorProdutoDeletarResponse>(response, this);
    }
}
