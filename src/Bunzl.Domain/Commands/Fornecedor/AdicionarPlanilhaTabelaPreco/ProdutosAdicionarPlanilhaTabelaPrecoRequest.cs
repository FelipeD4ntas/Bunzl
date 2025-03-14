using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using DevExtreme.AspNet.Data;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaTabelaPreco;

public class ProdutosAdicionarPlanilhaTabelaPrecoRequest(Guid fornecedorId, IFormFile arquivo) : DataSourceLoadOptionsBase, IRequest<CommandResponse<DataSourcePageResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public IFormFile Arquivo { get; set; } = arquivo;
}
