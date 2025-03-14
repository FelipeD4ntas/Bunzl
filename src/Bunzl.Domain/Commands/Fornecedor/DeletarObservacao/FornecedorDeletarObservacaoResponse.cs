using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.DeletarObservacao;

public class FornecedorDeletarObservacaoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
