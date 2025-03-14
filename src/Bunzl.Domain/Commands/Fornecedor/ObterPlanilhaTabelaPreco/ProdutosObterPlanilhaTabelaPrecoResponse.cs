namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaTabelaPreco;

public class ProdutosObterPlanilhaTabelaPrecoResponse(string? nome, string? tipo, string? base64)
{
    public string? Nome { get; set; } = nome;
    public string? Tipo { get; set; } = tipo;
    public string? Base64 { get; set; } = base64;
}
