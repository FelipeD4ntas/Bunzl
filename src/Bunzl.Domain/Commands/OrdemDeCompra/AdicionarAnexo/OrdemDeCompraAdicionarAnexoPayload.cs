using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;

public class OrdemDeCompraAdicionarAnexoPayload
{
    public required IFormFile Arquivo { get; set; }

    public OrdemDeCompraAdicionarAnexoRequest ToRequest(Guid ordemDeCompraId)
    {
        return new OrdemDeCompraAdicionarAnexoRequest(ordemDeCompraId, Arquivo);
    }
}
