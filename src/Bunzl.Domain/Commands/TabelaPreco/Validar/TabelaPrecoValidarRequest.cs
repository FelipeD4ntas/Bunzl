using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Validar;

public class TabelaPrecoValidarRequest(Guid id, DateTime dataFimVigencia) : IRequest<CommandResponse<TabelaPrecoValidarResponse>>
{
    public Guid Id { get; set; } = id;
    public DateTime DataFimVigencia { get; set; } = dataFimVigencia;
}