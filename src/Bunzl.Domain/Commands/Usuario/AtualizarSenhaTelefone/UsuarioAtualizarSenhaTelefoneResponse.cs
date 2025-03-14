using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Usuario.AtualizarSenhaTelefone;

public class UsuarioAtualizarSenhaTelefoneResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);