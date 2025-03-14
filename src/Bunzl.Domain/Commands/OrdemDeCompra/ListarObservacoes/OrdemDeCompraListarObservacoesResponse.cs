using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.OrdemDeCompra.ListarObservacoes;

public class OrdemDeCompraListarObservacoesResponse
{
    public IEnumerable<OrdemDeCompraObservacaoListarDto>? OrdemDeCompraObservacoes { get; set; }
}
