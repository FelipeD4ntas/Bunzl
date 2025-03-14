namespace Bunzl.Core.Domain.DTOs.Gateway;

public class GatewayTabelaPrecoDto
{
    public string CnpjEmpresa { get; set; }
    public string CodigoFornecedor { get; set; }
    public string? CodigoTabelaPreco { get; set; }
    public string DescricaoTabelaPreco { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public string DataInicialValidade { get; set; }
    public string DataFinalValidade { get; set; }
    public List<GatewayTabelaPrecoProdutoDto> Produtos { get; set; } = [];
}

