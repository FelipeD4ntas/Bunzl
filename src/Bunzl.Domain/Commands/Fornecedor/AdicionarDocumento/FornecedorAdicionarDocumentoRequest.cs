using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarDocumento;

public class FornecedorAdicionarDocumentoRequest(Guid fornecedorId, IFormFile arquivo, string? observacao) : IRequest<CommandResponse<FornecedorAdicionarDocumentoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
    public IFormFile Arquivo { get; set; } = arquivo;
    public string? Observacao { get; set; } = observacao;
}