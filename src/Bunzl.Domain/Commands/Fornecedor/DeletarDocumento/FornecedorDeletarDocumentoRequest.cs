using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarDocumento;

public class FornecedorDeletarDocumentoRequest : IRequest<CommandResponse<FornecedorDeletarDocumentoResponse>>
{
    public Guid Id { get; set; }
    public Guid DocumentoId {  get; set; }
}
