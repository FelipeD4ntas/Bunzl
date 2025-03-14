using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;

public class TabelaPrecoListarProdutosHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco) : Notifiable, IRequestHandler<TabelaPrecoListarProdutosRequest, CommandResponse<List<TabelaPrecoListarProdutosResponse>>>
{
    public async Task<CommandResponse<List<TabelaPrecoListarProdutosResponse>>> Handle(TabelaPrecoListarProdutosRequest request, CancellationToken cancellationToken)
    {
        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(false, p => p.Id == request.TabelaPrecoId, cancellationToken, "Produtos", "Produtos.Produto");

        if (tabelaPreco is null)
        {
            AddNotification("Tabela de Preço", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<List<TabelaPrecoListarProdutosResponse>>(this);
        }

        var response = tabelaPreco.Produtos.Select(tpp => new TabelaPrecoListarProdutosResponse
        {
            Id = tpp.Id,
            Produto = new ProdutoDto
            {
                Id = tpp.ProdutoId,
                CodigoSku = tpp.Produto.CodigoSku!,
                CodigoFornecedor = tpp.Produto.CodigoFornecedor!,
                DescricaoCompletaBunzl = tpp.Produto.DescricaoCompletaBunzl!,
                UnidadeMedida = tpp.Produto.UnidadeMedidaBunzl!
            },
            UltimoPrecoPraticado = tpp.UltimoPrecoPraticado,
            NovoPreco = tpp.NovoPreco,
            Status = tpp.Status
        }).ToList();

        return new CommandResponse<List<TabelaPrecoListarProdutosResponse>>(response, this);
    }
}