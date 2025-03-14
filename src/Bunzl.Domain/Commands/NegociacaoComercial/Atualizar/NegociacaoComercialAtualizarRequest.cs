using Bunzl.Domain.DTOs;
using Bunzl.Infra.CrossCutting.MediatoR.DTOs;
using MediatR;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;

public class NegociacaoComercialAtualizarRequest(
	Guid id,
	Guid fornecedorId, 
	string? titulo,
	DateTime dataEntrega,
	string? campoAtuacao,
	string termosPagamento,
	decimal valorTotal,
	List<NegociacaoComercialProdutoDto>? produtos
	) : IRequest<CommandResponse<NegociacaoComercialAtualizarResponse>>
{
	public Guid Id { get; set; } = id;
	public Guid FornecedorId { get; set; } = fornecedorId;
	public string? Titulo { get; set; } = titulo;
	public DateTime DataEntrega { get; set; } = dataEntrega;
	public string? CampoAtuacao { get; set; } = campoAtuacao;
	public string TermosPagamento { get; set; } = termosPagamento;
	public decimal ValorTotal { get; set; } = valorTotal;

	public List<NegociacaoComercialProdutoDto>? Produtos { get; set; } = produtos;
}

