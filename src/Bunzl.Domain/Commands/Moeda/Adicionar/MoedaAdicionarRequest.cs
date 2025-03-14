using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Moeda.Adicionar;

public class MoedaAdicionarRequest : IRequest<CommandResponse<MoedaAdicionarResponse>>
{
    public required string Sigla { get; set; }
    public required string Descricao { get; set; }
}