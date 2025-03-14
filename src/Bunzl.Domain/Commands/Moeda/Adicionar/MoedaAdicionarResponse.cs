using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Moeda.Adicionar;

public class MoedaAdicionarResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
