using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarDocumento;

public class FornecedorDeletarDocumentoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
