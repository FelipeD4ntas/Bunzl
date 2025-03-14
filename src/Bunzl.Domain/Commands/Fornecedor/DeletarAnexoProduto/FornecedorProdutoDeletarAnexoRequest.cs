using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarAnexoProduto;

public class FornecedorProdutoDeletarAnexoRequest(Guid fornecedorId, Guid fornecedorProdutoId, Guid AnexoId) : IRequest<CommandResponse<FornecedorProdutoDeletarAnexoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid FornecedorProdutoId { get; set; } = fornecedorProdutoId;
    public Guid AnexoId { get; set; } = AnexoId;
}