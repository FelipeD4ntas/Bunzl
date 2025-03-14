using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.DTOs;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class NegociacaoComercial : EntityBase<Guid>, IAggregationRoot
{
    public long Codigo { get; set; }
    public Guid FornecedorId { get; set; } 
    public Guid EmpresaId { get; set; }
    public string? Titulo { get; set; }
    public DateTime DataEntrega { get; set; }
    public string? CampoAtuacao { get; set; }
    public string TermosPagamento { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }

	public EStatusNegociacaoComercial Status { get; set; } = EStatusNegociacaoComercial.EmNegociacao;
    public virtual List<NegociacaoComercialProduto> Produtos { get; set; } = [];
    public virtual List<NegociacaoComercialAnexo> Anexos { get; set; } = [];
    public virtual List<NegociacaoComercialObservacao> NegociacaoComercialObservacoes { get; set; } = [];

    public virtual Fornecedor Fornecedor { get; set; }

	protected NegociacaoComercial()
    {
    }

    public NegociacaoComercial(long codigo, Guid fornecedorId, Guid empresaId, string titulo, DateTime dataEntrega,
        string campoAtuacao, string termosPagamento, decimal valorTotal)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        FornecedorId = fornecedorId;
        EmpresaId = empresaId;
        Titulo = titulo;
        DataEntrega = dataEntrega;
        CampoAtuacao = campoAtuacao;
        TermosPagamento = termosPagamento;
        ValorTotal = valorTotal;
    }

    public void Atualizar(string titulo, DateTime dataEntrega, string campoAtuacao, string termosPagamento, decimal valorTotal)
    {
		Titulo = titulo;
		DataEntrega = dataEntrega;
		CampoAtuacao = campoAtuacao;
		TermosPagamento = termosPagamento;
        ValorTotal = valorTotal;
        Status = EStatusNegociacaoComercial.EmNegociacao;
	}

    public void AtualizarStatus(EStatusNegociacaoComercial status)
    {
        Status = status;
    }

    public void AtualizarProdutos(List<NegociacaoComercialProdutoDto> produtosDto)
    {
	    Produtos.Clear();
	    foreach (var produtoDto in produtosDto)
	    {

			Produtos.Add(new NegociacaoComercialProduto
		    (
			    produtoDto.ProdutoId,
                produtoDto.CodigoSku,
                produtoDto.Descricao,
                produtoDto.ValorUnitarioOriginal,
                produtoDto.Quantidade,
                produtoDto.ValorUnitarioNegociado,
                produtoDto.Observacao,
                Id,
                produtoDto.ValorUnitarioAlvo,
                produtoDto.ValorUnitarioFinal
            ));
	    }
    }

	public void AdicionarAnexo(NegociacaoComercialAnexo anexo)
    {
        Anexos.Add(anexo);
    }

    public void DeletarAnexo(NegociacaoComercialAnexo anexo)
    {
        Anexos.Remove(anexo);
    }

    public void AdicionarObservacao(NegociacaoComercialObservacao negociacaoComercialObservacao)
    {
		NegociacaoComercialObservacoes.Add(negociacaoComercialObservacao);
    }

    public void DeletarObservacao(NegociacaoComercialObservacao negociacaoComercialObservacao)
    {
        NegociacaoComercialObservacoes.Remove(negociacaoComercialObservacao);
    }
}

