using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AdicionarAnexo;

public class NegociacaoComercialAdicionarAnexoPayload
{
    public required IFormFile Arquivo { get; set; }
    public string? Observacao { get; set; }

    public NegociacaoComercialAdicionarAnexoRequest ToRequest(Guid negociacaoComercialId)
    {
        return new NegociacaoComercialAdicionarAnexoRequest(negociacaoComercialId, Arquivo, Observacao);
    }
}
