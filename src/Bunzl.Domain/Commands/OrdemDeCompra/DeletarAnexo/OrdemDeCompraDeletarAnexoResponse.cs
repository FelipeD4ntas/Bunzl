using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.OrdemDeCompra.DeletarAnexo;

public class OrdemDeCompraDeletarAnexoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
