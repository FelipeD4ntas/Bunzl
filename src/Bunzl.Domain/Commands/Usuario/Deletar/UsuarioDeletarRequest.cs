using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Usuario.Deletar
{
    public class UsuarioDeletarRequest : IRequest<CommandResponse<UsuarioDeletarResponse>>
    {
        public Guid Id { get; set; }
    }
}
