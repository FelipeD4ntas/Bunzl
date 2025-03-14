using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;

public class FornecedorAdicionarObservacaoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem)
{

}