using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ListarHomologadoComTabelaPreco;

public class FornecedorListarHomologadoComTabelaPrecoRequest : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
}
