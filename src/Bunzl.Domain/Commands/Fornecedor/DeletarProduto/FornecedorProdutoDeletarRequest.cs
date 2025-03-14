using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarProduto;

public class FornecedorProdutoDeletarRequest(Guid id, Guid produtoId) : IRequest<CommandResponse<FornecedorProdutoDeletarResponse>>
{
    [JsonIgnore]
    public Guid FornecedorId { get; set; } = id;

    [JsonIgnore]
    public Guid ProdutoId { get; set; } = produtoId;
}
