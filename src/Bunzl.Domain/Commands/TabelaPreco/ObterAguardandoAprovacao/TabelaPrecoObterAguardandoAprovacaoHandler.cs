using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;

public class TabelaPrecoObterAguardandoAprovacaoHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco) : Notifiable, IRequestHandler<TabelaPrecoObterAguardandoAprovacaoRequest, CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>>
{
    public async Task<CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>> Handle(TabelaPrecoObterAguardandoAprovacaoRequest request, CancellationToken cancellationToken)
    {
        //Eduardo
        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(false, p => p.FornecedorId == request.FornecedorId && p.Status == EStatusTabelaPreco.AguardandoAprovacao, cancellationToken, "Produtos", "Produtos.Produto");

        if (tabelaPreco is null)
        {
            AddNotification("Tabela de Preço", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>(this);
        }

        var response = new TabelaPrecoObterAguardandoAprovacaoResponse
        {
            Id = tabelaPreco.Id,
            DataInicioVigencia = tabelaPreco.DataInicioVigencia,
            DataFimVigencia = tabelaPreco.DataFimVigencia,
            CodigoERP = tabelaPreco.CodigoERP,
            Status = tabelaPreco.Status,
            FlagExpirada = tabelaPreco.FlagExpirada,
            Produtos = tabelaPreco.Produtos.Select(tpp => new TabelaPrecoProdutoObterDto
            {
                Id = tpp.Id,
                Produto = new ProdutoDto
                {
                    Id = tpp.ProdutoId,
                    CodigoSku = tpp.Produto.CodigoSku!,
                    DescricaoCompletaBunzl = tpp.Produto.DescricaoCompletaBunzl!,
                    CodigoFornecedor = tpp.Produto.CodigoFornecedor,
                    DescricaoCompletaFornecedor = tpp.Produto.DescricaoCompletaFornecedor
                },
                UltimoPrecoPraticado = tpp.UltimoPrecoPraticado,
                NovoPreco = tpp.NovoPreco,
                Status = tpp.Status
            }).ToList()
        };

        return new CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>(response, this);
    }
}