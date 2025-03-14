using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.NegociacaoComercial.DeletarAnexo;

public class NegociacaoComercialDeletarAnexoResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
