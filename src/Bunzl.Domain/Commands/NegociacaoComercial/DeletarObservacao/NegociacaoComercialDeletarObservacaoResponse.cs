using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarObservacao;

public class NegociacaoComercialDeletarObservacaoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
