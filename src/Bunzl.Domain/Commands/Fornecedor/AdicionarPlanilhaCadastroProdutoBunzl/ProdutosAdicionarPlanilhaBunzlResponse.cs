using Bunzl.Domain.DTOs;
using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;

public class ProdutosAdicionarPlanilhaBunzlResponse(Guid id, string mensagem, List<FornecedorProdutoErrosDto> fornecedorProdutoErrosCadastro, int quantidadeProdutosAssociados = 0) : BaseResponseDto(id, mensagem)
{
    public List<FornecedorProdutoErrosDto>? FornecedorProdutoErrosCadastro { get; set; } = fornecedorProdutoErrosCadastro;
    public int QuantidadeProdutosAssociados { get; set; } = quantidadeProdutosAssociados;
}
