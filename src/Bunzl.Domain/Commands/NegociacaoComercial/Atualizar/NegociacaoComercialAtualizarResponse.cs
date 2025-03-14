using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;

public class NegociacaoComercialAtualizarResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem)
{
}
