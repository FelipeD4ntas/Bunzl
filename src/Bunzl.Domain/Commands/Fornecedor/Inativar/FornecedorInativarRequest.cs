using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Inativar;

public class FornecedorInativarRequest : IRequest<CommandResponse<FornecedorInativarResponse>>
{
    public Guid Id { get; set; }
}
