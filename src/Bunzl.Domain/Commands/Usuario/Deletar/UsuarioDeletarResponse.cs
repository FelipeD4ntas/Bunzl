using Bunzl.Domain.DTOs.Base;

namespace Bunzl.Domain.Commands.Usuario.Deletar;

public class UsuarioDeletarResponse(Guid id, string mensagem) : BaseResponseDto(id, mensagem);
