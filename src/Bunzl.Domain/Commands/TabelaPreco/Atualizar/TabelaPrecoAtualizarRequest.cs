using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Atualizar;

public class TabelaPrecoAtualizarRequest(Guid id, DateTime dataFimVigencia, List<TabelaPrecoProdutoAtualizarDto> produtos) : IRequest<CommandResponse<TabelaPrecoAtualizarResponse>>
{
    public Guid Id { get; set; } = id;
    public DateTime DataFimVigencia { get; set; } = dataFimVigencia;
    public List<TabelaPrecoProdutoAtualizarDto> Produtos { get; set; } = produtos;
}