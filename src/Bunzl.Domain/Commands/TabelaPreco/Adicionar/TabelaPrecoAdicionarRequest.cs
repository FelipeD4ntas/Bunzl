using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.TabelaPreco.Adicionar;

public class TabelaPrecoAdicionarRequest : IRequest<CommandResponse<TabelaPrecoAdicionarResponse>>
{
    public Guid FornecedorId { get; set; }
    public virtual List<TabelaPrecoProdutoAdicionarDto> Produtos { get; set; } = [];
}