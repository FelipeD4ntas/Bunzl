using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Validar;

public class TabelaPrecoValidarHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco) : Notifiable, IRequestHandler<TabelaPrecoValidarRequest, CommandResponse<TabelaPrecoValidarResponse>>
{
    public async Task<CommandResponse<TabelaPrecoValidarResponse>> Handle(TabelaPrecoValidarRequest request, CancellationToken cancellationToken)
    {
        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(true, p => p.Id == request.Id, cancellationToken, p => p.Produtos, p => p.Fornecedor);

        if (tabelaPreco is null)
        {
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<TabelaPrecoValidarResponse>(this);
        }

        if (tabelaPreco.Produtos.Any(p => p.Status == EStatusTabelaPrecoProduto.AguardandoAprovacao))
        {
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoPodeSerFinalizadaPoisContemProdutosAguardandoAprovacao);
            return new CommandResponse<TabelaPrecoValidarResponse>(this);
        }

        tabelaPreco.Status = tabelaPreco.Produtos.Any(p => p.Status == EStatusTabelaPrecoProduto.Reprovada) ? EStatusTabelaPreco.Reprovada : EStatusTabelaPreco.Validada;
        tabelaPreco.DataFimVigencia = request.DataFimVigencia;
        repositoryTabelaPreco.Update(tabelaPreco);

        if (tabelaPreco.Status == EStatusTabelaPreco.Reprovada)
            return new CommandResponse<TabelaPrecoValidarResponse>(new TabelaPrecoValidarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoValidadaComSucesso, tabelaPreco.Fornecedor, false), this);

        return new CommandResponse<TabelaPrecoValidarResponse>(new TabelaPrecoValidarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoValidadaComSucesso, tabelaPreco.Fornecedor, true), this);
    }
}