using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Inativar
{
    public class UsuarioInativarRequest : IRequest<CommandResponse<UsuarioInativarResponse>>
    {
        public Guid Id { get; set; }
    }
}
