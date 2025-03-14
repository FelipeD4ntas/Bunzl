using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;

public class ProdutosAdicionarPlanilhaTabelaPrecoPayload
{
    public required IFormFile Arquivo { get; set; }

    public ProdutosAdicionarPlanilhaTabelaPrecoRequest ToRequest(Guid fornecedorId)
    {
        return new ProdutosAdicionarPlanilhaTabelaPrecoRequest(fornecedorId, Arquivo);
    }
}
