using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarProdutosHomologado;

public class FornecedorProdutoListarHomologadoRequest(Guid fornecedorId) : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
}
