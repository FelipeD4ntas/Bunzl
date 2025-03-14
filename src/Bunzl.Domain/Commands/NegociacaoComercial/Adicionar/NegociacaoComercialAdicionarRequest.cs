using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;
using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Adicionar;

public class NegociacaoComercialAdicionarRequest : IRequest<CommandResponse<NegociacaoComercialAdicionarResponse>>
{
    public Guid FornecedorId { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataEntrega { get; set; }
    public string? CampoAtuacao { get; set; }
    public decimal ValorTotal { get; set; }
    public string TermosPagamento { get; set; }
    public List<NegociacaoComercialProdutoDto>? Produtos { get; set; } = [];
    public List<NegociacaoComercialObservacaoDto>? Observacoes { get; set; } = [];
    public List<NegociacaoComercialAnexoDto>? Anexos { get; set; } = [];
}