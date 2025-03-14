using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProdutoBunzl;

public class ProdutosObterPlanilhaBunzlRequest(Guid fornecedorId, bool semSku) : IRequest<CommandResponse<ProdutosObterPlanilhaBunzlResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public bool SemSku { get; set; } = semSku;
}
