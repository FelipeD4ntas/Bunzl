using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginInicial;

public class LoginInicialRequest : IRequest<CommandResponse<LoginInicialResponse>>
{
    public required string Email { get; set; }
    public required string Senha { get; set; }
}