using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarPlanilhaCadastroProduto;

public class ProdutosAdicionarPlanilhaRequest(IFormFile arquivo, Guid fornecedorId) : IRequest<CommandResponse<ProdutosAdicionarPlanilhaResponse>>
{
    public IFormFile Arquivo { get; set; } = arquivo;
    public Guid FornecedorId { get; set; } = fornecedorId;
}
