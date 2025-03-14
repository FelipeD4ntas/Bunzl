using Bunzl.Domain.DTOs;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Obter;

public class NegociacaoComercialObterResponse(
	Guid id,
	Guid fornecedorId,
	string nomeFornecedor,
	string titulo,
	DateTime dataEntrega,
	DateTime dataCriacao,
	string campoAtuacao,
	string termosPagamento,
	decimal valorTotal,
	long codigo,
	List<NegociacaoComercialProdutoObterDto> produtos,
	List<NegociacaoComercialAnexoObterDto> anexos)
{
	public Guid Id { get; set; } = id;
	public Guid FornecedorId { get; set; } = fornecedorId;
	public string NomeFornecedor { get; set; } = nomeFornecedor;
	public string? Titulo { get; set; } = titulo;
	public DateTime DataEntrega { get; set; } = dataEntrega;
	public DateTime DataCriacao { get; set; } = dataCriacao;
	public string? CampoAtuacao { get; set; } = campoAtuacao;
	public string TermosPagamento { get; set; } = termosPagamento;
	public decimal ValorTotal { get; set; } = valorTotal;
	public long Codigo { get; set; } = codigo;
	public List<NegociacaoComercialProdutoObterDto> Produtos { get; set; } = produtos;
	public List<NegociacaoComercialAnexoObterDto> Anexos { get; set; } = anexos;
}

