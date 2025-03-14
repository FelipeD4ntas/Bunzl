using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;
using Bunzl.Infra.Data.Repositories.Base;
using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.Entities;
using DevExtreme.AspNet.Data;
using Bunzl.Domain.DTOs;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Bunzl.Infra.Data.Repositories;

public class RepositoryOrdemDeCompra(BunzlContext context) : RepositoryBase<OrdemDeCompra, BunzlContext, Guid>(context), IRepositoryOrdemDeCompra, IInjectScoped 
{
    public async Task<DataSourcePageResponse> ListDevExpressOrdemDeCompraAnexoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase,
        Guid? ordemDeCompraId)
    {
        Expression<Func<OrdemDeCompraAnexo, bool>> where = fd => EF.Property<Guid>(fd, "OrdemDeCompraId") == ordemDeCompraId;
        var set = Context.Set<OrdemDeCompraAnexo>();
        var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<OrdemDeCompraAnexoListarDto>(loadResult);
    }

    public async Task RemoverProdutosPorOrdemDeCompraIdAsync(Guid ordemDeCompraId, CancellationToken cancellationToken)
    {
	    var produtos = await Context.Set<OrdemDeCompraProduto>()
		    .Where(p => p.OrdemDeCompraId == ordemDeCompraId)
		    .ToListAsync(cancellationToken);

	    Context.Set<OrdemDeCompraProduto>().RemoveRange(produtos);
	    await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverUnidadesMedidaPorOrdemDeCompraIdAsync(Guid ordemDeCompraId, CancellationToken cancellationToken)
    {
		var unidadesMedida = await Context.Set<OrdemDeCompraUnidadeMedida>()
			.Where(p => p.OrdemDeCompraId == ordemDeCompraId)
			.ToListAsync(cancellationToken);

		Context.Set<OrdemDeCompraUnidadeMedida>().RemoveRange(unidadesMedida);
		await Context.SaveChangesAsync(cancellationToken);
	}

    public async Task<OrdemDeCompraAnexo?> ObterAnexoAsync(Guid anexoId)
    {
        return await Context.Set<OrdemDeCompraAnexo>().FirstOrDefaultAsync(p => p.Id == anexoId);
    }
}

