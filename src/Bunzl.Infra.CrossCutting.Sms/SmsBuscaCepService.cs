using Bunzl.Core.Domain.DTOs.Sms;
using Bunzl.Core.Domain.Interfaces.Sms;
using Bunzl.Infra.CrossCutting.ConexaoClient;
using Bunzl.Infra.CrossCutting.Extensions;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.Sms.Response;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Xml.Serialization;

namespace Bunzl.Infra.CrossCutting.Sms;

public class SmsBuscaCepService(IConfiguration configuration, IConexaoHttpClient conexaoClient) : ISmsBuscaCepService, IInjectScoped
{
    private const int ValorResultadoSucesso = 1;
    private readonly string _urlBase = configuration["SmsBuscaCepSettings:BaseUrl"]!;
    private readonly string _chave = configuration["SmsBuscaCepSettings:Key"]!;
    private readonly string _nomeExibicao = configuration["SmsBuscaCepSettings:DisplayMessage"]!;

    public async Task<EnviarSmsDto> EnviarSms(string area, string telefone, string texto)
    {
        try
        {
            var builder = new UriBuilder(_urlBase);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["telefone"] = (area + telefone).ApenasNumeros();
            query["texto"] = texto;
            query["chave"] = _chave;
            builder.Query = query.ToString();
            var urlComParametros = builder.ToString();

            var response = await conexaoClient.SendRequestAsync(HttpMethod.Get, urlComParametros, null, null, null);
            var responseContent = await response.Content.ReadAsStringAsync();

            var serializer = new XmlSerializer(typeof(SmsBuscaCepResponse));
            using (var reader = new StringReader(responseContent))
            {
                var result = (SmsBuscaCepResponse?)serializer.Deserialize(reader);
                if (result == null || result.Resultado != ValorResultadoSucesso)
                {
                    return await Task.FromResult(new EnviarSmsDto(false, result?.ResultadoTxt ?? "Erro ao enviar o SMS."));
                }
            }

            return await Task.FromResult(new EnviarSmsDto(true, "SMS enviado com sucesso!"));
        }
        catch (Exception ex)
        {
            var mensagem = ex.InnerException == null ? "Erro ao enviar o SMS: " + ex.Message : "Erro ao enviar o SMS: " + ex.Message + " - " + ex.InnerException.Message;
            return await Task.FromResult(new EnviarSmsDto(false, mensagem));
        }
    }

    public async Task<EnviarSmsDto> EnviarSmsCodigoOtp(string area, string telefone, string codigoOtp)
    {
        var texto = $"{_nomeExibicao} - Seu codigo de verificacao para iniciar a sessao é {codigoOtp}. Nao o compartilhe com ninguem.";
        return await EnviarSms(area, telefone, texto);
    }
}
