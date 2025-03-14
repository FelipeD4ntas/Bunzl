using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Login.LoginAlterarEmpresa;

public class LoginAlterarEmpresaRequest : IRequest<CommandResponse<LoginAlterarEmpresaResponse>>
{
    public required Guid? EmpresaId { get; set; }
    public required string Idioma { get; set; }
}