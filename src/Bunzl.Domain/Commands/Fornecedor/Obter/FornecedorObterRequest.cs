using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.Obter;

public class FornecedorObterRequest(Guid id) : IRequest<CommandResponse<FornecedorObterResponse>>
{
    public Guid Id { get; set; } = id;
}
