using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;

public class TabelaPrecoObterAguardandoAprovacaoRequest(Guid fornecedorId) : IRequest<CommandResponse<TabelaPrecoObterAguardandoAprovacaoResponse>>
{
    public Guid FornecedorId { get; set; } = fornecedorId;
}