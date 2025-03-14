using Bunzl.Core.Domain.DTOs.Gateway;

namespace Bunzl.Core.Domain.Interfaces.ExternalService;

public interface IExternalServiceFornecedor
{
    Task<IEnumerable<GatewayFornecedoresDto>> ObterTodosFornecedores(string empresaCnpj, CancellationToken cancellationToken);
    Task<IEnumerable<GatewayFornecedoresDto>> ObterFornecedoresPorCodigo(string empresaCnpj, string codigoFornecedor, CancellationToken cancellationToken);
    Task<IEnumerable<GatewayFornecedoresDto>> ObterFornecedorePorDataInicioDataFim(string empresaCnpj, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim, CancellationToken cancellationToken);
}
