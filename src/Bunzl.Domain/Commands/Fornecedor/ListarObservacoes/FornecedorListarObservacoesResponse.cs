using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.Fornecedor.ListarObservacoes;

public class FornecedorListarObservacoesResponse
{
    public IEnumerable<FornecedorObservacaoDto>? FornecedorObservacoes { get; set; }
}
