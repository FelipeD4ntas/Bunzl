using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Integracao.ObterFornecedores;

public class ObterFornecedoresResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem)
{
}
