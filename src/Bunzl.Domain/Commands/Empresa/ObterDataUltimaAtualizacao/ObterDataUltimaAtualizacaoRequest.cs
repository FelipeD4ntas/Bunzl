using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;

public class ObterDataUltimaAtualizacaoRequest(Guid id) : IRequest<CommandResponse<ObterDataUltimaAtualizacaoResponse>>
{
    public Guid Id { get; set; } = id;
}

