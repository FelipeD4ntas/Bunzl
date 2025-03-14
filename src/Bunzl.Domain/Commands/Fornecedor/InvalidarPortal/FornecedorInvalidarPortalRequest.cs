using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.InvalidarPortal;

public class FornecedorInvalidarPortalRequest : IRequest<CommandResponse<FornecedorInvalidarPortalResponse>>
{
    public Guid Id { get; set; }
}
