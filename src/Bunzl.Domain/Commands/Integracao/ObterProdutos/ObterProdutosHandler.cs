using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Domain.Interfaces.Services;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using Bunzl.Infra.CrossCutting.NotificationPattern;
using MediatR;
using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Infra.CrossCutting.Resources;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.Helper;

namespace Bunzl.Domain.Commands.Integracao.ObterProdutos;

public class ObterProdutosHandler(
    IExternalServiceFornecedorProduto externalServiceFornecedorProduto,
    IRepositoryFornecedor repositoryFornecedor,
    IRepositoryUsuario repositoryUsuario,
    IRepositoryEmpresa repositoryEmpresa,
    IUsuarioFornecedorService usuarioFornecedorService
    )
    : Notifiable, IRequestHandler<ObterProdutosRequest, CommandResponse<DataSourcePageResponse>>
{
    public async Task<CommandResponse<DataSourcePageResponse>> Handle(ObterProdutosRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<GatewayFornecedorProdutosDto> produtos = [];

        var empresas = await repositoryEmpresa.ListAsync(true, 0, 100, e => request.EmpresaCnpj == e.Cnpj);
        if (empresas == null || !empresas.Any())
        {
            AddNotification("Empresas", UsuarioResources.UsuarioFalhaRelacionarEmpresas);
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        if (!string.IsNullOrEmpty(request.CodigoSKU))
        {
            produtos = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedorProduto.ObterProdutoPorCodigoSKU(request.EmpresaCnpj, request.CodigoSKU, cancellationToken),
                AddNotification,
                "Produto",
                IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos))!;
        }
        else if (request.DataAlteracaoInicio is not null && request.DataAlteracaoFim is not null)
        {
            produtos = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedorProduto.ObterProdutoPorDataInicioDataFim(request.EmpresaCnpj, request.DataAlteracaoInicio, request.DataAlteracaoFim, cancellationToken),
                AddNotification,
                "Produto",
                IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos))!;
        }
        else
        {
            produtos = (await SafeExecutionHelper.SafeExecuteAsync(
                () => externalServiceFornecedorProduto.ObterTodosProdutos(request.EmpresaCnpj, cancellationToken),
                AddNotification,
                "Produto",
                IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos))!;

            var empresaAutenticada = empresas.FirstOrDefault();

            if (empresaAutenticada is not null && produtos is not null)
            {
                empresaAutenticada.DataUltimaAtualizacao = DateTime.Now;

                repositoryEmpresa.Update(empresaAutenticada);
            }
        }

        if (produtos == null || !produtos.Any())
        {
            return new CommandResponse<DataSourcePageResponse>(this);
        }

        var produtosResponse = new DataSourcePageResponse
        {
            Data = produtos.Select(p => new GatewayFornecedorProdutosDto
            {
                SKU = p.SKU,
                Descricao = p.Descricao,
                CodigoArtigo = p.CodigoArtigo,
                Familia = p.Familia,
                Cor = p.Cor,
                Tamanho = p.Tamanho,
                UnidadeMedida = p.UnidadeMedida,
                DataAlteracao = p.DataAlteracao,
            }).ToList(),
            Summary = null
        };

		return new CommandResponse<DataSourcePageResponse>(produtosResponse, this);
    }
}
