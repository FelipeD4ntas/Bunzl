using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginGerarCodigoOtp;

public class LoginGerarCodigoOtpRequest : IRequest<CommandResponse<LoginGerarCodigoOtpResponse>>
{
    public required Guid? Id { get; set; }
}