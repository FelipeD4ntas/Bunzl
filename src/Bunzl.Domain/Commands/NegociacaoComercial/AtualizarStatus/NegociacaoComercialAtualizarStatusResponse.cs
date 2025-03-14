using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;

public class NegociacaoComercialAtualizarStatusResponse(Guid id, string mensagem, bool aceitaIntegrada = false) : BaseResponseDto(id, mensagem)
{
	public bool AceitaIntegrada { get; set; } = aceitaIntegrada;
}

