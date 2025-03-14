using Bunzl.Core.Domain.DTOs.Gateway;
using Bunzl.Infra.CrossCutting.NotificationPattern.Interface;

namespace Bunzl.Core.Domain.Interfaces.ExternalService;

public interface IExternalServiceTabelaPreco : INotifiable
{
    Task<GatewayRetornoTabelaPrecoDto> IntegrarTabelaPrecoErp(GatewayTabelaPrecoDto tabelaPreco, bool flagRegravarTabelaPreco, CancellationToken cancellationToken);
    //Task<GatewayRetornoTabelaPrecoDto> SalvarTabelaPrecoErp(GatewayTabelaPrecoDto tabelaPreco, CancellationToken cancellationToken);
    //Task<GatewayRetornoTabelaPrecoDto> AtualizarTabelaPrecoErp(GatewayTabelaPrecoDto tabelaPreco, CancellationToken cancellationToken);
}
