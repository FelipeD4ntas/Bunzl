using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterAnexoProduto;

public class FornecedorProdutoObterAnexoRequest(Guid fornecedorId, Guid fornecedorProdutoId, Guid anexoId) : IRequest<CommandResponse<FornecedorProdutoObterAnexoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid FornecedorProdutoId { get; set; } = fornecedorProdutoId;
    public Guid AnexoId { get; set; } = anexoId;
}
