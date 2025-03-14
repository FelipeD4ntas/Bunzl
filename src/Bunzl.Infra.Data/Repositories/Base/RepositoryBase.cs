using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bunzl.Infra.Data.Repositories.Base;

public class RepositoryBase<TEntity, TContext, TId> : IRepositoryBase<TEntity, TId>
    where TEntity : EntityBase<TId>, IAggregationRoot
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(TContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    #region Exists

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, bool ignoreFilters = false,
        CancellationToken cancellationToken = default)
    {
        var query = ignoreFilters ? DbSet.IgnoreQueryFilters().AsNoTracking() : DbSet.AsNoTracking();
        return await query.AnyAsync(where, cancellationToken);
    }


    #endregion

    #region Count

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().CountAsync(where, cancellationToken);
    }

    #endregion

    #region List

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take)
    {
        return tracking
            ? await DbSet.Skip(skip).Take(take).ToListAsync()
            : await DbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Skip(skip).Take(take).ToListAsync()
            : await query.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking,
        params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Skip(skip).Take(take).ToListAsync()
            : await query.AsNoTracking().Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        Expression<Func<TEntity, bool>> where)
    {
        return tracking
            ? await DbSet.Where(where).Skip(skip).Take(take).ToListAsync()
            : await DbSet.AsNoTracking().Where(where).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Where(where).Skip(skip).Take(take).ToListAsync()
            : await query.AsNoTracking().Where(where).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        Expression<Func<TEntity, bool>> where, params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Where(where).Skip(skip).Take(take).ToListAsync()
            : await query.AsNoTracking().Where(where).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where, bool ignoreFilters = false)
    {
        var query = ignoreFilters ? DbSet.IgnoreQueryFilters().AsQueryable() : DbSet.AsQueryable();
        return tracking
            ? await query.Where(where).ToListAsync()
            : await query.AsNoTracking().Where(where).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Where(where).ToListAsync()
            : await query.AsNoTracking().Where(where).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.Where(where).ToListAsync()
            : await query.AsNoTracking().Where(where).ToListAsync();
    }

    #endregion

    #region ListDevExpress

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase)
    {
        var source = tracking
            ? DbSet.AsQueryable()
            : DbSet.AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);

        var source = tracking
            ? query
            : query.AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);

        var source = tracking
            ? query
            : query.AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where)
    {
        var source = tracking
            ? DbSet.Where(where)
            : DbSet.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);

        var source = tracking
            ? query.Where(where)
            : query.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }

    public async Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where, params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);

        var source = tracking
            ? query.Where(where)
            : query.Where(where).AsNoTracking().AsQueryable();

        var loadResult = await DataSourceLoader.LoadAsync(source, dataSourceLoadOptionsBase ?? new DataSourceLoadOptionsBase());
        return ConvertDataSourceLoaderToDevExpressPageResponse<T>(loadResult);
    }
    #endregion

    #region GetBy

    public async Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where, bool ignoreFilters = false,
        CancellationToken cancellationToken = default)
    {
        var query = ignoreFilters ? DbSet.IgnoreQueryFilters() : DbSet;
        return tracking
            ? await query.FirstOrDefaultAsync(where, cancellationToken)
            : await query.AsNoTracking().FirstOrDefaultAsync(where, cancellationToken);
    }

    public async Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where, bool ignoreFilters,
       CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);

        return tracking
            ? await query.IgnoreQueryFilters().FirstOrDefaultAsync(where, cancellationToken)
            : await query.IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(where, cancellationToken);
    }

    public async Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.FirstOrDefaultAsync(where, cancellationToken)
            : await query.AsNoTracking().FirstOrDefaultAsync(where, cancellationToken);
    }

    public async Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default, params string[] navigationProperties)
    {
        var query = DbSet.AsQueryable();
        query = Include(query, navigationProperties);
        return tracking
            ? await query.FirstOrDefaultAsync(where, cancellationToken)
            : await query.AsNoTracking().FirstOrDefaultAsync(where, cancellationToken);
    }

    #endregion

    #region Add

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddCollectionAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    #endregion

    #region Update

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public void UpdateCollection(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }

    #endregion

    #region Delete

    public void DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void DeleteCollectionAsync(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    #endregion

    #region Inclusão de Propriedades de Navegação

    private IQueryable<TEntity> Include(IQueryable<TEntity> query,
        params Expression<Func<TEntity, object>>[] incluirPropriedadesNavegacao)
    {
        return incluirPropriedadesNavegacao.Aggregate(query, (current, property) => current.Include(property));
    }

    private IQueryable<TEntity> Include(IQueryable<TEntity> query, params string[] incluirPropriedadesNavegacao)
    {
        return incluirPropriedadesNavegacao.Aggregate(query, (current, property) => current.Include(property));
    }

    #endregion

    #region Converter Objetos DevExpress

    protected DataSourcePageResponse ConvertDataSourceLoaderToDevExpressPageResponse<T>(LoadResult loadResult)
    {
        return new DataSourcePageResponse
        {
            Data = loadResult.data.Adapt<IEnumerable<T>>(),
            TotalCount = loadResult.totalCount,
            GroupCount = loadResult.groupCount,
            Summary = loadResult.summary
        };
    }

    #endregion
}