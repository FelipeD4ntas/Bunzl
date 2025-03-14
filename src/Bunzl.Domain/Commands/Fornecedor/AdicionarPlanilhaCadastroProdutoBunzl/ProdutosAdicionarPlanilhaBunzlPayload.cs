using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;

public class ProdutosAdicionarPlanilhaBunzlPayload
{
    public required IFormFile Arquivo { get; set; }
    public ProdutosAdicionarPlanilhaBunzlRequest ToRequest(Guid fornecedorId)
    {
        return new ProdutosAdicionarPlanilhaBunzlRequest(fornecedorId, Arquivo);
    }
}
