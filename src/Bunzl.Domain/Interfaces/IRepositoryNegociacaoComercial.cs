using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities;
using DevExtreme.AspNet.Data;

namespace Bunzl.Domain.Interfaces;

public interface IRepositoryNegociacaoComercial : IRepositoryBase<NegociacaoComercial, Guid>
{
    Task<DataSourcePageResponse> ListDevExpressNegociacaoComercialAnexoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Guid? negociacaoComercialId);
}
