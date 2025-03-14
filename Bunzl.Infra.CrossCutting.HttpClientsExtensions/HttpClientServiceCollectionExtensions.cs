using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Bunzl.Infra.CrossCutting.HttpClientsExtensions;

public static class HttpClientServiceCollectionExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection service, IConfiguration config)
    {
        service.AddHttpClient();

        #region Gateway
        var gatewayConfiguration = config.GetSection(nameof(GatewayConfiguration)).Get<GatewayConfiguration>();

        if (gatewayConfiguration != null)
        {
            service.AddOptions<GatewayConfiguration>()
                .BindConfiguration(nameof(GatewayConfiguration));

            service.AddHttpClient("Gateway", opt =>
            {
                opt.BaseAddress = new Uri(gatewayConfiguration.BaseUrl ?? "");

                if (!string.IsNullOrEmpty(gatewayConfiguration.UserName) && !string.IsNullOrEmpty(gatewayConfiguration.Password))
                {
                    var credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{gatewayConfiguration.UserName}:{gatewayConfiguration.Password}"));
                    opt.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                }
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));
        }

        return service;
        #endregion
    }
}
