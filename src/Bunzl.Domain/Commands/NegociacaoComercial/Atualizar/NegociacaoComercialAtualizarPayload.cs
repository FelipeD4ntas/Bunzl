using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Atualizar;

public class NegociacaoComercialAtualizarPayload
{
	public Guid FornecedorId { get; set; }
	public string? Titulo { get; set; }
	public DateTime DataEntrega { get; set; }
	public string? CampoAtuacao { get; set; } 
	public string TermosPagamento { get; set; } 
	public decimal ValorTotal { get; set; }	
	public List<NegociacaoComercialProdutoDto>? Produtos { get; set; }

	public NegociacaoComercialAtualizarRequest ToRequest(Guid id)
	{
		return new NegociacaoComercialAtualizarRequest(
			id,
			FornecedorId,
			Titulo,
			DataEntrega,
			CampoAtuacao,
			TermosPagamento,
			ValorTotal,
			Produtos
			);
	}
}

