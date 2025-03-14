namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProdutoBunzl;

public class ProdutosObterPlanilhaBunzlResponse(string? nome, string? tipo, string? base64)
{
    public string? Nome { get; set; } = nome;
    public string? Tipo { get; set; } = tipo;
    public string? Base64 { get; set; } = base64;
}
