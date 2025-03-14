using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.ListarProdutos;

public class TabelaPrecoListarProdutosRequest(Guid tabelaPrecoId) : IRequest<CommandResponse<List<TabelaPrecoListarProdutosResponse>>>
{
    public Guid TabelaPrecoId { get; set; } = tabelaPrecoId;
}