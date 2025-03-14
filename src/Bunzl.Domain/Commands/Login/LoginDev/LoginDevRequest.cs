using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginDev;

public class LoginDevRequest : IRequest<CommandResponse<LoginDevResponse>>
{
}