using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;
using Bunzl.Infra.Data.Repositories.Base;
using DevExtreme.AspNet.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Bunzl.Infra.Data.Repositories;

public class RepositoryNegociacaoComercial(BunzlContext context) : RepositoryBase<NegociacaoComercial, BunzlContext, Guid>(context), IRepositoryNegociacaoComercial, IInjectScoped
{
    public async Task<DataSourcePageResponse> ListDevExpressNegociacaoComercialAnexoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Guid? negociacaoComercialId)
    {
        Expression<Func<NegociacaoComercialAnexo, bool>> where = fd => EF.Property<Guid>(fd, "NegociacaoComercialId") == negociacaoComercialId;
        var set = Context.Set<NegociacaoComercialAnexo>();
        var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<NegociacaoComercialAnexoListarDto>(loadResult);
    }
}

