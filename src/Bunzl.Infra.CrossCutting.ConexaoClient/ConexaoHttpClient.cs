using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Bunzl.Infra.CrossCutting.ConexaoClient;

public class ConexaoHttpClient : IConexaoHttpClient, IInjectScoped
{
    private readonly HttpClient _httpClient;

    public ConexaoHttpClient()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, string? token, string? tipoToken, object? data = null)
    {
        var request = new HttpRequestMessage(method, url);

        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = string.IsNullOrWhiteSpace(tipoToken) ? new AuthenticationHeaderValue(token) : new AuthenticationHeaderValue(tipoToken, token);

        if (data != null && method == HttpMethod.Post)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        return response;
    }
}
