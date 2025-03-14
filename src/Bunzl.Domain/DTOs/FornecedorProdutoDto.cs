using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.DTOs;

public class FornecedorProdutoDto
{
    public Guid Id { get; set; }
    public string CodigoFornecedor { get; set; }
    public string? CodigoSku { get; set; }
    public string DescricaoCompletaFornecedor { get; set; }
    public string? DescricaoCompletaBunzl { get; set; }
    public decimal Preco { get; set; }
    public EStatusProduto Status { get; set; }
}
