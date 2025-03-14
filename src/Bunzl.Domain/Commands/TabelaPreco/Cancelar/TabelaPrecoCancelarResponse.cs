using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.TabelaPreco.Cancelar;

public class TabelaPrecoCancelarResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
