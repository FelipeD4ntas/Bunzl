using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Entities;
using DevExtreme.AspNet.Data;
using System.Linq.Expressions;

namespace Bunzl.Domain.Interfaces;

public interface IRepositoryFornecedor : IRepositoryBase<Fornecedor, Guid>
{
    Task<DataSourcePageResponse> ListDevExpressFornecedorDocumentoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Guid? fornecedorId);
    Task<DataSourcePageResponse> ListDevExpressFornecedorProdutosAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<FornecedorProduto, bool>> where);
    Task<DataSourcePageResponse> ListDevExpressFornecedorProdutosAnexosAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<FornecedorProdutoAnexo, bool>> where);
    Task<DataSourcePageResponse> ListDevExpressFornecedorTabelasPrecoAsync<T>(bool tracking, DataSourceLoadOptionsBase? dataSourceLoadOptionsBase, Expression<Func<TabelaPreco, bool>> where);
}