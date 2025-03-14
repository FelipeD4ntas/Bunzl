using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.VerificarResetSenha;

public class UsuarioVerificarResetSenhaRequest : IRequest<CommandResponse<UsuarioVerificarResetSenhaResponse>>
{
    public required Guid Chave { get; set; }
}