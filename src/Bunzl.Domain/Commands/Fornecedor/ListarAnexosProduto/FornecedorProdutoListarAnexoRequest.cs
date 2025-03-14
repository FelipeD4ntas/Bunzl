using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarAnexosProduto;

public class FornecedorProdutoListarAnexoRequest(Guid fornecedorId, Guid fornecedorProdutoId) : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid FornecedorProdutoId { get; set; } = fornecedorProdutoId;
}
