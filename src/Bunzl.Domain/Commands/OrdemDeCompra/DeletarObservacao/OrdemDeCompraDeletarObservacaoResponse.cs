using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarObservacao;

public class OrdemDeCompraDeletarObservacaoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
