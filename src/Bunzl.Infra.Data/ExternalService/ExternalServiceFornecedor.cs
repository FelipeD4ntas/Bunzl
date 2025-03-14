using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.Resources;
using System.Text.Json;

namespace Bunzl.Infra.Data.ExternalService;

public class ExternalServiceFornecedor(IHttpClientFactory clientFactory) : IExternalServiceFornecedor, IInjectScoped
{
    public async Task<IEnumerable<GatewayFornecedoresDto>> ObterFornecedorePorDataInicioDataFim(string empresaCnpj, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = $"api/fornecedores?dataAlteracaoInicio={dataAlteracaoInicio:yyyy-MM-ddTHH:mm:ss}&dataAlteracaoFim={dataAlteracaoFim:yyyy-MM-ddTHH:mm:ss}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);

        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(IntegracaoResources.FornecedorNaoEncontradoPorPeriodo);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var fornecedores = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedoresDto>>(content);

        return fornecedores;
    }

    public async Task<IEnumerable<GatewayFornecedoresDto>> ObterFornecedoresPorCodigo(string empresaCnpj, string codigoFornecedor, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = $"api/fornecedores/{codigoFornecedor}";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);
        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(IntegracaoResources.FornecedorNaoEncontradoPorCodigo);
        }

        IEnumerable<GatewayFornecedoresDto> fornecedores = [];
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(content) || content == "[\n  \"\"\n]")
        {
            fornecedores = Enumerable.Empty<GatewayFornecedoresDto>();
        }
        else
        {
            fornecedores = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedoresDto>>(content);
        }

        return fornecedores;
    }

    public async Task<IEnumerable<GatewayFornecedoresDto>> ObterTodosFornecedores(string empresaCnpj, CancellationToken cancellationToken)
    {
        var client = clientFactory.CreateClient("Gateway");

        var requestUri = "api/fornecedores";

        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Add("X-CNPJ", empresaCnpj);

        var response = await client.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(IntegracaoResources.FornecedorNaoEncontrado);
        }

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var fornecedores = JsonSerializer.Deserialize<IEnumerable<GatewayFornecedoresDto>>(content);
            return fornecedores;
        }
        catch (Exception e)
        {
            var x = e;
            throw;
        }

    }
}
