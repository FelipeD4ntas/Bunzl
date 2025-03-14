using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.SolicitarResetSenha;

public class UsuarioSolicitarResetSenhaRequest(string email) : IRequest<CommandResponse<UsuarioSolicitarResetSenhaResponse>>
{
    public string Email { get; } = email;
}