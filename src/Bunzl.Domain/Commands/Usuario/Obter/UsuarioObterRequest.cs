using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Obter
{
    public class UsuarioObterRequest(Guid id) : IRequest<CommandResponse<UsuarioObterResponse>>
    {
        public Guid Id { get; set; } = id;
    }
}
