using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.ObterDocumento;

public class FornecedorObterDocumentoRequest(Guid id, Guid documentoId) : IRequest<CommandResponse<FornecedorObterDocumentoResponse>>
{
    public Guid Id { get; set; } = id;
    public Guid DocumentoId { get; set; } = documentoId;
}
