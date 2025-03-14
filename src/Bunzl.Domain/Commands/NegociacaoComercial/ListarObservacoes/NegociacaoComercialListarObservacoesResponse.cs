using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ListarObservacoes;

public class NegociacaoComercialListarObservacoesResponse
{
    public IEnumerable<NegociacaoComercialObservacoesDto>? NegociacaoComercialObservacoes { get; set; }
}
