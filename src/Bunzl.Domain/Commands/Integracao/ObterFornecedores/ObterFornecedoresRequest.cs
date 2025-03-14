using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Integracao.ObterFornecedores;

public class ObterFornecedoresRequest(string empresaCnpj, string? codigoFornecedor, DateTime? dataAlteracaoInicio, DateTime? dataAlteracaoFim) : IRequest<CommandResponse<ObterFornecedoresResponse>>
{
    public string EmpresaCnpj { get; set; } = empresaCnpj;
    public string? CodigoFornecedor { get; set; } = codigoFornecedor;
    public DateTime? DataAlteracaoInicio { get; set; } = dataAlteracaoInicio;
    public DateTime? DataAlteracaoFim { get; set; } = dataAlteracaoFim;

}
