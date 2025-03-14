namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProduto;

public class ProdutosObterPlanilhaResponse(string? nome, string? tipo, string? base64)
{
    public string? Nome { get; set; } = nome;
    public string? Tipo { get; set; } = tipo;
    public string? Base64 { get; set; } = base64;
}
