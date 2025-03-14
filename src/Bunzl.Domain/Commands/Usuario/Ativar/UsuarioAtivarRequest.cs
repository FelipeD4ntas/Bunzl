using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Ativar
{
    public class UsuarioAtivarRequest : IRequest<CommandResponse<UsuarioAtivarResponse>>
    {
        public Guid Id { get; set; }
    }
}
