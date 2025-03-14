using Bunzl.Domain.Commands.SignUp;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;

namespace Bunzl.Application.Interfaces;

public interface ISignUpAppService
{
    Task<CommandResponse<SignUpResponse>> SignUp(SignUpRequest request);
}