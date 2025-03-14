using Bunzl.Core.Domain.DTOs.DevExpress;
using DevExtreme.AspNet.Data;
using System.Linq.Expressions;

namespace Bunzl.Core.Domain.Interfaces.Base;

public interface IRepositoryBase<TEntity, TId>
    where TEntity : class, IAggregationRoot
{
    #region Exists

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> where, bool ignoreFilters = false, CancellationToken cancellationToken = default);

    #endregion

    #region Count

    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    #endregion

    #region List

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take,
        params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking,
        params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, params string[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take, params string[] navigationProperties);
    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take, Expression<Func<TEntity, bool>> where);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take, Expression<Func<TEntity, bool>> where,
        params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, int skip, int take, Expression<Func<TEntity, bool>> where,
        params string[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where, bool ignoreFilters = false);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<IEnumerable<TEntity>> ListAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        params string[] navigationProperties);

    #endregion

    #region List DevExpress

    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase);

    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, params string[] navigationProperties);

    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where);
    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<DataSourcePageResponse> ListDevExpressAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TEntity, bool>> where, params string[] navigationProperties);

    #endregion

    #region GetBy

    Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        bool ignoreFilters = false, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where, bool ignoreFilters = false,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] navigationProperties);

    Task<TEntity?> GetByAsync(bool tracking, Expression<Func<TEntity, bool>> where,
        CancellationToken cancellationToken = default, params string[] navigationProperties);

    #endregion

    #region Add

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddCollectionAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    #endregion

    #region Update

    void Update(TEntity entity);
    void UpdateCollection(IEnumerable<TEntity> entities);

    #endregion

    #region Delete

    void DeleteAsync(TEntity entity);
    void DeleteCollectionAsync(IEnumerable<TEntity> entities);

    #endregion
}