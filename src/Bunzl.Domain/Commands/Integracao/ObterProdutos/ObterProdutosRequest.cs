using Bunzl.Core.Domain.DTOs.DevExpress;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Integracao.ObterProdutos;

public class ObterProdutosRequest(string empresaCnpj, string? codigoSKU, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim) : IRequest<CommandResponse<DataSourcePageResponse>>
{
    public string EmpresaCnpj { get; set; } = empresaCnpj;
    public string? CodigoSKU { get; set; } = codigoSKU;
    public DateTime? DataAlteracaoInicio { get; set; } = dataAlteracaoInicio;
    public DateTime? DataAlteracaoFim { get; set; } = dataAlteracaoFim;

}
