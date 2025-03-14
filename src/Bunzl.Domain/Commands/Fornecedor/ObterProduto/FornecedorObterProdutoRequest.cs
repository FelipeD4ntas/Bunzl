using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterProduto;

public class FornecedorObterProdutoRequest(Guid fornecedorId, Guid ProdutoId) : IRequest<CommandResponse<FornecedorObterProdutoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid ProdutoId { get; set; } = ProdutoId;
}

