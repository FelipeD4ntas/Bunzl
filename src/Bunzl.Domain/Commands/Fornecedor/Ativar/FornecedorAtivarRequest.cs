using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Ativar;

public class FornecedorAtivarRequest : IRequest<CommandResponse<FornecedorAtivarResponse>>
{
    public Guid Id { get; set; }
}
