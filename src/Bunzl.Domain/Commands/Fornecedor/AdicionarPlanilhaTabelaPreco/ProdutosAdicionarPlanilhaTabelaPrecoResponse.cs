using Bunzl.Domain.DTOs;
using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;

public class ProdutosAdicionarPlanilhaTabelaPrecoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem)
{
    public IEnumerable<FornecedorProdutoTabelaPrecoDto>? ProdutosTabelaPreco { get; set; }
}
