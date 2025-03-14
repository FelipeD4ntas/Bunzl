using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Infra.CrossCutting.Resources;
using System.Text.Json;

namespace Bunzl.Infra.Data.ExternalService;

public class ExternalServiceOrdemDeCompra(IHttpClientFactory clientFactory) : IExternalServiceOrdemDeCompra, IInjectScoped
{
	public async Task<IEnumerable<GatewayOrdemDeCompraDto>> ObterOrdemDeCompraPorDataInicioDataFim(string empresaCnpj, DateTime? dataInicio, DateTime? dataFim,
		CancellationToken cancellationToken)
	{
		var client = clientFactory.CreateClient("Gateway");
        client.Timeout = TimeSpan.FromSeconds(1200);

        var requestUri = $"api/ordem-compra?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}";

		var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
		request.Headers.Add("X-CNPJ", empresaCnpj);

		var response = await client.SendAsync(request, cancellationToken);

		if (!response.IsSuccessStatusCode)
			throw new HttpRequestException(IntegracaoResources.CnpjEmpresaNaoEncontrouOrdemDeCompra);

		var content = await response.Content.ReadAsStringAsync(cancellationToken);
        try
        {
            var ordemDeCompra = JsonSerializer.Deserialize<IEnumerable<GatewayOrdemDeCompraDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return ordemDeCompra ?? [];
        }
        catch (Exception e)
        {
            var teste = e;
            return new List<GatewayOrdemDeCompraDto>();
        }
	}
}