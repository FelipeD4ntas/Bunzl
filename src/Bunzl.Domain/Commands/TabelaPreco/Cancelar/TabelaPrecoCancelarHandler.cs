using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Cancelar;

public class TabelaPrecoCancelarHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IRepositoryProduto repositoryProduto) : Notifiable, IRequestHandler<TabelaPrecoCancelarRequest, CommandResponse<TabelaPrecoCancelarResponse>>
{
    public async Task<CommandResponse<TabelaPrecoCancelarResponse>> Handle(TabelaPrecoCancelarRequest request, CancellationToken cancellationToken)
    {
        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(true, p => p.Id == request.Id, cancellationToken, p => p.Produtos);

        if (tabelaPreco is null)
        {
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<TabelaPrecoCancelarResponse>(this);
        }

        //Definindo status do produtos da tabela de preço como "Cancelada"
        tabelaPreco.Produtos.ForEach(tpp => tpp.Status = EStatusTabelaPrecoProduto.Cancelada);

        tabelaPreco.Status = EStatusTabelaPreco.Cancelada;
        
        repositoryTabelaPreco.Update(tabelaPreco);

        return new CommandResponse<TabelaPrecoCancelarResponse>(new TabelaPrecoCancelarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoCanceladaComSucesso), this);
    }
}