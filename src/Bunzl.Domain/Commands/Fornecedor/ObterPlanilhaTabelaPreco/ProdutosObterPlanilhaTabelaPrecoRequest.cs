using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterPlanilhaTabelaPreco;

public class ProdutosObterPlanilhaTabelaPrecoRequest(Guid fornecedorId) : IRequest<CommandResponse<ProdutosObterPlanilhaTabelaPrecoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
}
