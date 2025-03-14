using Bunzl.Core.Domain.DTOs.Gateway;

namespace Bunzl.Core.Domain.Interfaces.ExternalService;

public interface IExternalServiceFornecedorProduto
{
    Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterTodosProdutos(string empresaCnpj, CancellationToken cancellationToken);
    Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterProdutoPorCodigoSKU(string empresaCnpj, string codigoSku, CancellationToken cancellationToken);
    Task<IEnumerable<GatewayFornecedorProdutosDto>> ObterProdutoPorDataInicioDataFim(string empresaCnpj, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim, CancellationToken cancellationToken);
}

