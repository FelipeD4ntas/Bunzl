using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginFinal;

public class LoginFinalRequest : IRequest<CommandResponse<LoginFinalResponse>>
{
    public required Guid? Id { get; set; }
    public required string? CodigoOtp { get; set; }
    public required string Idioma { get; set; } = "pt-BR";
}