using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Entities;
using Bunzl.Domain.Interfaces;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.Data.Context;
using Bunzl.Infra.Data.Repositories.Base;
using DevExtreme.AspNet.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bunzl.Infra.Data.Repositories;

public class RepositoryFornecedor(BunzlContext context) : RepositoryBase<Fornecedor, BunzlContext, Guid>(context), IRepositoryFornecedor, IInjectScoped
{
    public async Task<DataSourcePageResponse> ListDevExpressFornecedorDocumentoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Guid? fornecedorId)
    {
        Expression<Func<FornecedorDocumento, bool>> where = fd => EF.Property<Guid>(fd, "FornecedorId") == fornecedorId;
        var set = Context.Set<FornecedorDocumento>();
        var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<FornecedorDocumentoDto>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressFornecedorProdutosAnexosAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<FornecedorProdutoAnexo, bool>> where)
    {
        var set = Context.Set<FornecedorProdutoAnexo>();
        var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<FornecedorProdutoAnexoDto>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressFornecedorProdutosAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<FornecedorProduto, bool>> where)
    {
        var set = Context.Set<FornecedorProduto>();

		var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<FornecedorProdutoDto>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressFornecedorTabelasPrecoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TabelaPreco, bool>> where)
    {
        var set = Context.Set<TabelaPreco>();
        var source = tracking
            ? set.Where(where)
            : set.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());

        return ConvertDataSourceLoaderToDevExpressPageResponse<TabelaPrecoHistoricoDto>(loadResult);
    }
}