using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.NegociacaoComercial.ListarAnexos;

public class NegociacaoComercialListarAnexosResponse
{
    public IEnumerable<NegociacaoComercialAnexoListarDto>? Anexos { get; set; }
}
