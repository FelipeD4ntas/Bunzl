using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.DTOs.TabelaPreco;

public class TabelaPrecoProdutoAtualizarDto
{
    public Guid Id { get; set; }
    public EStatusTabelaPrecoProduto Status { get; set; }
}
