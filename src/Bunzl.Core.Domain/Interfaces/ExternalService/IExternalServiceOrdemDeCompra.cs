using Bunzl.Core.Domain.DTOs.Gateway;

namespace Bunzl.Core.Domain.Interfaces.ExternalService;

public interface IExternalServiceOrdemDeCompra
{
	Task<IEnumerable<GatewayOrdemDeCompraDto>> ObterOrdemDeCompraPorDataInicioDataFim(string empresaCnpj, DateTime? dataInicio, DateTime? dataFim, CancellationToken cancellationToken);
}

