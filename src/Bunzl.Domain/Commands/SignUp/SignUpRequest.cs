using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.SignUp;

public class SignUpRequest : IRequest<CommandResponse<SignUpResponse>>
{
    public required string Nome { get; set; }
    public required string Email { get; set; }

}