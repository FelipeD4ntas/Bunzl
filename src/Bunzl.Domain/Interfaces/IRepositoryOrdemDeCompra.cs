using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities;
using DevExtreme.AspNet.Data;

namespace Bunzl.Domain.Interfaces;

public interface IRepositoryOrdemDeCompra : IRepositoryBase<OrdemDeCompra, Guid>
{
    Task<DataSourcePageResponse> ListDevExpressOrdemDeCompraAnexoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Guid? ordemDeCompraId);
    Task RemoverProdutosPorOrdemDeCompraIdAsync(Guid ordemDeCompraId, CancellationToken cancellationToken);
    Task RemoverUnidadesMedidaPorOrdemDeCompraIdAsync(Guid ordemDeCompraId, CancellationToken cancellationToken);
    Task<OrdemDeCompraAnexo?> ObterAnexoAsync(Guid anexoId);
}

