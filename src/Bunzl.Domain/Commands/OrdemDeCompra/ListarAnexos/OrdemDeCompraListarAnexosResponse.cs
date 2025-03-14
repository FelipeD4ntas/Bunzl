using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ListarAnexos;

public class OrdemDeCompraListarAnexosResponse
{
    public IEnumerable<OrdemDeCompraAnexoListarDto>? Anexos { get; set; }
}
