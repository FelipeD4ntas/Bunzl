using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;

public class FornecedorAdicionarObservacaoRequest(Guid id, string observacao) : IRequest<CommandResponse<FornecedorAdicionarObservacaoResponse>>
{
    public Guid FornecedorId { get; set; } = id;
    public string Observacao { get; set; } = observacao;
}