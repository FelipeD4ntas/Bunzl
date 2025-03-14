using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.ResetSenha;

public class UsuarioResetSenhaRequest : IRequest<CommandResponse<UsuarioResetSenhaResponse>>
{
    public required Guid Chave { get; set; }
    public required string NovaSenha { get; set; }
    public required string ConfirmacaoNovaSenha { get; set; }
}