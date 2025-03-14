using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Core.Domain.Interfaces.ExternalService;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bunzl.Infra.CrossCutting.NotificationPattern;

namespace Bunzl.Infra.Data.ExternalService;
public class ExternalServiceTabelaPreco(IHttpClientFactory clientFactory) : Notifiable, IExternalServiceTabelaPreco, IInjectScoped
{
    public async Task<GatewayRetornoTabelaPrecoDto> IntegrarTabelaPrecoErp(GatewayTabelaPrecoDto tabelaPreco, bool flagRegravarTabelaPrecoErp, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var client = clientFactory.CreateClient("Gateway");

        // TODO: Mudar endpoint quando tiverem terminado a API do COSMOS (retirar o -test)
        //var requestUri = $"api/tabela-precos-test";
        var requestUri = $"api/tabela-precos";
        var jsonContent = JsonSerializer.Serialize(tabelaPreco, options);
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response;

        if (flagRegravarTabelaPrecoErp)
        {
            if (string.IsNullOrEmpty(tabelaPreco.CodigoTabelaPreco))
                response = await client.PostAsync(requestUri, content, cancellationToken);
            else
                response = await client.PutAsync(requestUri, content, cancellationToken);
        }
        else
            response = await client.PostAsync(requestUri, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            AddNotification("API Gateway", await response.Content.ReadAsStringAsync(cancellationToken));
            return new GatewayRetornoTabelaPrecoDto();
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var resultado = JsonSerializer.Deserialize<GatewayRetornoTabelaPrecoDto>(responseContent);

        return resultado!;
    }
}
