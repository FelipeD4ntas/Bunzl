using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Commands.Fornecedor.Atualizar;
using Bunzl.Domain.Enumerators;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.Helper;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using Bunzl.Infra.CrossCutting.Resources;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Aprovar;

public class TabelaPrecoAprovarHandler(
    IRepositoryTabelaPreco repositoryTabelaPreco,
    IRepositoryProduto repositoryProduto,
    IRepositoryFornecedor repositoryFornecedor,
    IExternalServiceTabelaPreco externalServiceTabelaPreco) : Notifiable, IRequestHandler<TabelaPrecoAprovarRequest, CommandResponse<TabelaPrecoAprovarResponse>>
{
    public async Task<CommandResponse<TabelaPrecoAprovarResponse>> Handle(TabelaPrecoAprovarRequest request, CancellationToken cancellationToken)
    {
        var tabelaPreco = await repositoryTabelaPreco.GetByAsync(true, p => p.Id == request.Id, cancellationToken, p => p.Fornecedor, p => p.Empresa, p => p.Produtos);
        if (tabelaPreco is null)
        {
            AddNotification("TabelaPreco", TabelaPrecoResources.TabelaPrecoNaoEncontrada);
            return new CommandResponse<TabelaPrecoAprovarResponse>(this);
        }

        if (string.IsNullOrEmpty(tabelaPreco.Fornecedor.CodigoFornecedor))
        {
            AddNotification("Fornecedor", FornecedorResources.FornecedorSemCodigoFornecedor);
            return new CommandResponse<TabelaPrecoAprovarResponse>(this);
        }

        //Inativando Tabela de Preço Anterior (Expirado = true)
        var tabelasPrecosVigentes = await repositoryTabelaPreco.ListAsync(true, p => p.FornecedorId == tabelaPreco.FornecedorId && p.EmpresaId == tabelaPreco.EmpresaId && p.Status == EStatusTabelaPreco.Integrada && p.FlagExpirada == false);
        tabelasPrecosVigentes.ForEach(tpv =>
        {
            tpv.FlagExpirada = true;
            repositoryTabelaPreco.Update(tpv);
        });

        //Atualizando Preço no Portal
        tabelaPreco.Produtos.ForEach(tpp =>
        {
            var produto = repositoryProduto.GetByAsync(true, p => p.Id == tpp.ProdutoId , false, cancellationToken).Result;
            if (produto is not null)
            {
                if (tpp.Status == EStatusTabelaPrecoProduto.Aprovada)
                    produto.Preco = tpp.NovoPreco;
                else
                    produto.Status = EStatusProduto.Suspenso;
                
                repositoryProduto.Update(produto);
            }
        });

        //Executando a Integração com o ERP
        var gatewayTabelaPreco = new GatewayTabelaPrecoDto
        {
            CnpjEmpresa = tabelaPreco.Empresa.Cnpj,
            CodigoFornecedor = tabelaPreco.Fornecedor.CodigoFornecedor,
            CodigoTabelaPreco = tabelaPreco.Empresa.FlagRegravarTabelaPrecoErp ? $"{tabelaPreco.Empresa.SiglaEmpresa}:{tabelaPreco.Fornecedor.CodigoTabelaPreco}" : $"{tabelaPreco.Empresa.SiglaEmpresa}:{GerarCodigoAlfanumerico(4)}",
            DescricaoTabelaPreco = $"{tabelaPreco.Fornecedor.CodigoERP} - {tabelaPreco.Fornecedor.SiglaFornecedor}",
            DataAtualizacao = tabelaPreco.DataAlteracao,
            DataInicialValidade = tabelaPreco.DataInicioVigencia.ToString("yyyy-MM-dd"),
            DataFinalValidade = tabelaPreco.DataFimVigencia.ToString("yyyy-MM-dd"),
            Produtos = tabelaPreco.Produtos.Select(p => new GatewayTabelaPrecoProdutoDto
            {
                Sku = p.Produto.CodigoSku!,
                Preco = p.NovoPreco,
            }).ToList()
        };

        var responseTabelaPreco = await SafeExecutionHelper.SafeExecuteAsync(
         () => externalServiceTabelaPreco.IntegrarTabelaPrecoErp(gatewayTabelaPreco, tabelaPreco.Empresa.FlagRegravarTabelaPrecoErp, cancellationToken),
         AddNotification, "API", FornecedorResources.NaoFoiPossivelSalvarProdutoNoErp);

        ClearNotifications();
        AddNotifications(externalServiceTabelaPreco.Notifications);

        if (IsValid())
        {
            tabelaPreco.CodigoERP = responseTabelaPreco.CodigoTabelaPreco;
            tabelaPreco.Status = EStatusTabelaPreco.Integrada;
            repositoryTabelaPreco.Update(tabelaPreco);

            tabelaPreco.Fornecedor.CodigoTabelaPreco = responseTabelaPreco.CodigoTabelaPreco?.Split(':')[1];
            repositoryFornecedor.Update(tabelaPreco.Fornecedor);

            return new CommandResponse<TabelaPrecoAprovarResponse>(new TabelaPrecoAprovarResponse(tabelaPreco.Id, TabelaPrecoResources.TabelaPrecoAprovadaComSucesso, tabelaPreco.Fornecedor), this);
        }

        return new CommandResponse<TabelaPrecoAprovarResponse>(this);
    }

    private string GerarCodigoAlfanumerico(int tamanho)
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        return new string(Enumerable.Repeat(caracteres, tamanho)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }

}