namespace Bunzl.Domain.DTOs.TabelaPreco;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public string CodigoSku { get; set; }
    public string DescricaoCompletaBunzl { get; set; }
    public string CodigoFornecedor { get; set; }
    public string DescricaoCompletaFornecedor { get; set; }
    public string UnidadeMedida { get; set; }
}