using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.NegociacaoComercial.Listar;

public class NegociacaoComercialListarResponse(Guid id, FornecedorDto fornecedor, long codigo, string? titulo, DateTime dataEntrega, string? campoAtuacao, string termosPagamento, decimal valorTotal, EStatusNegociacaoComercial status)
{
	public Guid Id { get; set; } = id;
    public FornecedorDto Fornecedor { get; set; } = fornecedor;
    public long Codigo { get; set; } = codigo;
	public string? Titulo { get; set; } = titulo;
	public DateTime DataEntrega { get; set; } = dataEntrega;
	public string? CampoAtuacao { get; set; } = campoAtuacao;
	public string TermosPagamento { get; set; } = termosPagamento;
	public decimal ValorTotal { get; set; } = valorTotal;
    public EStatusNegociacaoComercial Status { get; set; } = status;
}

