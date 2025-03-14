namespace Bunzl.Domain.DTOs.TabelaPreco;

public class TabelaPrecoProdutoComErrosDto(string erro)
{
    public string Erro { get; set; } = erro;
}