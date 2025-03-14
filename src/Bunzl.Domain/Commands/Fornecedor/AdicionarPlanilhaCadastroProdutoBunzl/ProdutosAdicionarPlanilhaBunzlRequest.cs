using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProdutoBunzl;

public class ProdutosAdicionarPlanilhaBunzlRequest(Guid fornecedorId, IFormFile arquivo) : IRequest<CommandResponse<ProdutosAdicionarPlanilhaBunzlResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public IFormFile Arquivo { get; set; } = arquivo;
}
