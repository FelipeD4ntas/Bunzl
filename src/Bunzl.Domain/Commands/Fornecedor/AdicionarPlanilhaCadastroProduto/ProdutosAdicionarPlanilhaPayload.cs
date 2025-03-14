using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;

public class ProdutosAdicionarPlanilhaPayload
{
    public required IFormFile Arquivo { get; set; }

    public ProdutosAdicionarPlanilhaRequest ToRequest(Guid fornecedorId)
    {
        return new ProdutosAdicionarPlanilhaRequest(Arquivo, fornecedorId);
    }
}
