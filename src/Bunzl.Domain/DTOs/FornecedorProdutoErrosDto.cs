namespace Bunzl.Domain.DTOs;

public class FornecedorProdutoErrosDto
{
    public string? CodigoFornecedor { get; set; }
    public string? DescricaoProduto { get; set; }
    public List<string> Erros { get; set; }
}
