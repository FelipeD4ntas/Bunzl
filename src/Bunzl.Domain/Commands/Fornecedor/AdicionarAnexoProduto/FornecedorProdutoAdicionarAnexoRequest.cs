using Bunzl.Domain.Enumerators;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarAnexoProduto;

public class FornecedorProdutoAdicionarAnexoRequest(Guid fornecedorId, Guid fornecedorProdutoId, IFormFile arquivo, string? observacao, ETipoDocumento tipoDocumento) : IRequest<CommandResponse<FornecedorProdutoAdicionarAnexoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public Guid FornecedorProdutoId { get; set; } = fornecedorProdutoId;
    public IFormFile Arquivo { get; set; } = arquivo;
    public string? Observacao { get; set; } = observacao;
    public ETipoDocumento TipoDocumento { get; set; } = tipoDocumento;
}
