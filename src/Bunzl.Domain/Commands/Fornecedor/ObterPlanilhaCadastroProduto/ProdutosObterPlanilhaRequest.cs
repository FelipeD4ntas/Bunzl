using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaCadastroProduto;

public class ProdutosObterPlanilhaRequest : IRequest<CommandResponse<ProdutosObterPlanilhaResponse>>
{
}
