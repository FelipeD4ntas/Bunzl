namespace Bunzl.Infra.CrossCutting.ConexaoClient;

public interface IConexaoHttpClient
{
    Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, string? token, string? tipoToken, object? data = null);
}
