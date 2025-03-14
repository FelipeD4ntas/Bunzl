using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.Resources;
using System.Text.Json;

namespace Bunzl.Infra.Data.ExternalService;

public class ExternalServiceFornecedorProduto(IHttpClientFactory clientFactory) : IExternalServiceFornecedorProduto, IInjectScoped
{
    public async Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterProdutoPorCodigoSKU(string empresaCnpj, string codigoSku, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = $"api/produtos/{codigoSku}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);
        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos);
        }

        IEnumerable<GatewayFornecedorProdutosDto> produtos = [];
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(content) || content == "[\n  \"\"\n]")
        {
            produtos = Enumerable.Empty<GatewayFornecedorProdutosDto>();
        }
        else
        {
            produtos = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedorProdutosDto>>(content);
        }

        return produtos;
    }

    public async Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterProdutoPorDataInicioDataFim(string empresaCnpj, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = $"api/produtos?dataAlteracaoInicio={dataAlteracaoInicio:yyyy-MM-ddTHH:mm:ss}&dataAlteracaoFim={dataAlteracaoFim:yyyy-MM-ddTHH:mm:ss}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);

        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var produtos = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedorProdutosDto>>(content);

        return produtos;
    }

    public async Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterTodosProdutos(string empresaCnpj, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = "api/produtos";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);

        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(IntegracaoResources.CnpjEmpresaNaoEncontrouProdutos);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var produtos = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedorProdutosDto>>(content);

        return produtos;
    }
}
