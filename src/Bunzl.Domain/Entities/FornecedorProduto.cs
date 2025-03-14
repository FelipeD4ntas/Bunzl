using Bunzl.Core.Domain.Entities.Base;
using Bunzl.Core.Domain.Interfaces.Base;
using Bunzl.Domain.Commands.Fornecedor.AtualizarProduto;
using Bunzl.Domain.Entities.Validations;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Entities;

public class FornecedorProduto : EntityBase<Guid>, IAggregationRoot
{
    public Guid FornecedorId { get; set; }
    public string CodigoFornecedor { get; set; } = string.Empty; 
    public string DescricaoCompletaFornecedor {  get; set; } = string.Empty;
    public string? DescricaoCompletaBunzl { get; set; }
    public string? AplicacoesPrincipais { get; set; }
    public string? Composicao {  get; set; }
    public string? Tamanho { get; set; }
    public string? Cor { get; set; }
    public string? CodigoNCM { get; set; }
    public string UnidadeMedidaFornecedorMOQ { get; set; } = string.Empty;
    public string UnidadeMedidaFornecedorPreco { get; set; } = string.Empty;
    public string? UnidadeMedidaBunzl { get; set; }
    public int QuantidadeMinimaPedido { get; set; } = 0;
    public decimal Preco { get; set; } = decimal.Zero;
    public Guid? IncotermId { get; set; }
    public string? TermoPagamento { get; set; }
    public string? Observacoes { get; set; }
    public string? DetalhesEmbalagem { get; set; }
    public decimal? PesoBruto { get; set;}
    public decimal Comprimento { get; set; } = decimal.Zero;
    public decimal Largura { get; set; } = decimal.Zero;
    public decimal Altura { get; set; } = decimal.Zero;
    public decimal CBM { get; set; } = decimal.Zero;
    public int? TempoEntrega { get; set; }
    public decimal? CustoDesenvolvimentoEmbalagem { get; set;}
    public decimal? CustoRotulagemEmbalagem { get; set; }
    public string? PortoEmbarque {  get; set; }
    public int? QuantidadeCarregamentoContainer20Ft { get; set; }
    public int? QuantidadeCarregamentoContainer40Ft { get; set; }
    public int? QuantidadeCarregamentoContainer40Hc { get; set; }
    public string? CodigoArtigo { get; set; }
    public string? Familia { get; set; }
    public string? CodigoSku { get; set; }
    public string? CorBunzl { get; set; }
    public string? TamanhoBunzl { get; set; }
    public EStatusProduto Status { get; set; } = EStatusProduto.NaoHomologado;
    public string? TipoEmbalagemInterna { get; set; }
    public int QuantidadePorEmbalagemInterna {  get; set; }
    public string? TipoCaixaMaster { get; set; }
    public int QuantidadePorCaixaMaster { get; set; }
    public decimal CapacidadeMensalFabrica { get; set; }
	public string? UnidadeMedidaCapacidadeMensal { get; set; }
    public string? NomeFabrica { get; set; }
    public decimal CustoDetalhadoMateriaPrima { get; set; }
    public decimal CustoDetalhadoCombustivel { get; set; }
    public decimal CustoDetalhadoEmbalagem { get; set; }
    public decimal CustoDetalhadoMaoDeObra { get; set; }
    public decimal CustoDetalhadoEnergia { get; set; }
    public decimal CustoDetalhadoTransporte { get; set; }
	public virtual Fornecedor? Fornecedor { get; set; }
    public virtual List<FornecedorProdutoAnexo> FornecedorProdutoAnexos { get; set; } = [];
    public virtual Incoterm? Incoterm { get; set; }

    protected FornecedorProduto() { }

    public FornecedorProduto(
        Guid fornecedorId, 
        string codigoFornecedor,
        string descricaoCompletaFornecedor, 
        string? tamanho,
        string? cor,
        string? codigoNcm,
        string? tipoEmbalagemInterna,
        int quantidadePorEmbalagemInterna,
        string? tipoCaixaMaster,
        int quantidadePorCaixaMaster,
        decimal? pesoBruto,
        string? aplicacoesPrincipais,
        string? composicao,
        string? detalhesEmbalagem,
        decimal? custoDesenvolvimentoEmbalagem,
        decimal? custoRotulagemEmbalagem,
        int quantidadeMinimaPedido,
        string unidadeMedidaFornecedorMOQ,
        decimal preco,
        string unidadeMedidaFornecedorPreco,
        string? portoEmbarque,
        int? tempoEntrega,
        Guid? incotermId,
        decimal capacidadeMensalFabrica,
        string? unidadeMedidaCapacidadeMensal,
        decimal custoDetalhadoMateriaPrima,
        decimal custoDetalhadoCombustivel,
        decimal custoDetalhadoEmbalagem,
        decimal custoDetalhadoMaoDeObra,
        decimal custoDetalhadoEnergia,
        decimal custoDetalhadoTransporte,
        string? nomeFabrica,
        string? termoPagamento,
        string? observacoes,
        decimal comprimento,
        decimal largura,
        decimal altura,
        int? quantidadeCarregamentoContainer20Ft,
        int? quantidadeCarregamentoContainer40Ft,
        int? quantidadeCarregamentoContainer40Hc)
    {
        FornecedorId = fornecedorId;
        CodigoFornecedor = codigoFornecedor;
        DescricaoCompletaFornecedor = descricaoCompletaFornecedor;
        Tamanho = tamanho;
        Cor = cor;
        CodigoNCM = codigoNcm;
        TipoEmbalagemInterna = tipoEmbalagemInterna;
        QuantidadePorEmbalagemInterna = quantidadePorEmbalagemInterna;
        TipoCaixaMaster = tipoCaixaMaster;
        QuantidadePorCaixaMaster = quantidadePorCaixaMaster;
        PesoBruto = pesoBruto;
        AplicacoesPrincipais = aplicacoesPrincipais;
        Composicao = composicao;
        DetalhesEmbalagem = detalhesEmbalagem;
        CustoDesenvolvimentoEmbalagem = custoDesenvolvimentoEmbalagem;
        CustoRotulagemEmbalagem = custoRotulagemEmbalagem;
        QuantidadeMinimaPedido = quantidadeMinimaPedido;
        UnidadeMedidaFornecedorMOQ = unidadeMedidaFornecedorMOQ;
        Preco = preco;
        UnidadeMedidaFornecedorPreco = unidadeMedidaFornecedorPreco;
        PortoEmbarque = portoEmbarque;
        TempoEntrega = tempoEntrega;
        IncotermId = incotermId;
        CapacidadeMensalFabrica = capacidadeMensalFabrica;
        UnidadeMedidaCapacidadeMensal = unidadeMedidaCapacidadeMensal;
        CustoDetalhadoMateriaPrima = custoDetalhadoMateriaPrima;
        CustoDetalhadoCombustivel = custoDetalhadoCombustivel;
        CustoDetalhadoEmbalagem = custoDetalhadoEmbalagem;
        CustoDetalhadoMaoDeObra = custoDetalhadoMaoDeObra;
        CustoDetalhadoEnergia = custoDetalhadoEnergia;
        CustoDetalhadoTransporte = custoDetalhadoTransporte;
        NomeFabrica = nomeFabrica;
        TermoPagamento = termoPagamento;
        Observacoes = observacoes;
        Comprimento = comprimento;
        Largura = largura;
        Altura = altura;
        QuantidadeCarregamentoContainer20Ft = quantidadeCarregamentoContainer20Ft;
        QuantidadeCarregamentoContainer40Ft = quantidadeCarregamentoContainer40Ft;
        QuantidadeCarregamentoContainer40Hc = quantidadeCarregamentoContainer40Hc;
        CBM = CalcularCbm(comprimento, largura, altura);
    }

	public FornecedorProduto(
		Guid fornecedorId,
		string codigoFornecedor,
		string descricaoCompletaFornecedor,
		string? aplicacoesPrincipais,
		string? composicao,
		string? tamanho,
		string? cor,
		string? codigoNcm,
		string unidadeMedidaFornecedorMOQ,
		string unidadeMedidaFornecedorPreco,
		int quantidadeMinimaPedido,
		decimal preco,
		Guid? incotermId,
		string? termoPagamento,
		string? observacoes,
		string? detalhesEmbalagem,
		decimal? pesoBruto,
		decimal comprimento,
		decimal largura,
		decimal altura,
		int? tempoEntrega,
		decimal? custoDesenvolvimentoEmbalagem,
		decimal? custoRotulagemEmbalagem,
		string? portoEmbarque,
		int? quantidadeCarregamentoContainer20Ft,
		int? quantidadeCarregamentoContainer40Ft,
		int? quantidadeCarregamentoContainer40Hc,
		string? tipoEmbalagemInterna,
		int quantidadePorEmbalagemInterna,
		string? tipoCaixaMaster,
		int quantidadePorCaixaMaster,
		decimal capacidadeMensalFabrica,
		string? unidadeMedidaCapacidadeMensal,
		string? nomeFabrica,
		decimal custoDetalhadoMateriaPrima,
		decimal custoDetalhadoCombustivel,
		decimal custoDetalhadoEmbalagem,
		decimal custoDetalhadoMaoDeObra,
		decimal custoDetalhadoEnergia,
		decimal custoDetalhadoTransporte)
	{
		FornecedorId = fornecedorId;
		CodigoFornecedor = codigoFornecedor;
		DescricaoCompletaFornecedor = descricaoCompletaFornecedor;
		AplicacoesPrincipais = aplicacoesPrincipais;
		Composicao = composicao;
		Tamanho = tamanho;
		Cor = cor;
		CodigoNCM = codigoNcm;
		UnidadeMedidaFornecedorMOQ = unidadeMedidaFornecedorMOQ;
		UnidadeMedidaFornecedorPreco = unidadeMedidaFornecedorPreco;
		QuantidadeMinimaPedido = quantidadeMinimaPedido;
		Preco = preco;
		IncotermId = incotermId;
		TermoPagamento = termoPagamento;
		Observacoes = observacoes;
		DetalhesEmbalagem = detalhesEmbalagem;
		PesoBruto = pesoBruto;
		Comprimento = comprimento;
		Largura = largura;
		Altura = altura;
		TempoEntrega = tempoEntrega;
		CustoDesenvolvimentoEmbalagem = custoDesenvolvimentoEmbalagem;
		CustoRotulagemEmbalagem = custoRotulagemEmbalagem;
		PortoEmbarque = portoEmbarque;
		QuantidadeCarregamentoContainer20Ft = quantidadeCarregamentoContainer20Ft;
		QuantidadeCarregamentoContainer40Ft = quantidadeCarregamentoContainer40Ft;
		QuantidadeCarregamentoContainer40Hc = quantidadeCarregamentoContainer40Hc;
		TipoEmbalagemInterna = tipoEmbalagemInterna;
		QuantidadePorEmbalagemInterna = quantidadePorEmbalagemInterna;
		TipoCaixaMaster = tipoCaixaMaster;
		QuantidadePorCaixaMaster = quantidadePorCaixaMaster;
		CapacidadeMensalFabrica = capacidadeMensalFabrica;
		UnidadeMedidaCapacidadeMensal = unidadeMedidaCapacidadeMensal;
		NomeFabrica = nomeFabrica;
		CustoDetalhadoMateriaPrima = custoDetalhadoMateriaPrima;
		CustoDetalhadoCombustivel = custoDetalhadoCombustivel;
		CustoDetalhadoEmbalagem = custoDetalhadoEmbalagem;
		CustoDetalhadoMaoDeObra = custoDetalhadoMaoDeObra;
		CustoDetalhadoEnergia = custoDetalhadoEnergia;
		CustoDetalhadoTransporte = custoDetalhadoTransporte;
		CBM = CalcularCbm(comprimento, largura, altura);
	}

	public void Atualizar(FornecedorProdutoAtualizarRequest produtoAtualizado)
    {
        CodigoFornecedor = produtoAtualizado.CodigoFornecedor;
        DescricaoCompletaFornecedor = produtoAtualizado.DescricaoCompletaFornecedor;
        DescricaoCompletaBunzl = produtoAtualizado.DescricaoCompletaBunzl;
        AplicacoesPrincipais = produtoAtualizado.AplicacoesPrincipais;
        Composicao = produtoAtualizado.Composicao;
        Tamanho = produtoAtualizado.Tamanho;
        Cor = produtoAtualizado.Cor;
        CodigoNCM = produtoAtualizado.CodigoNCM;
        UnidadeMedidaFornecedorMOQ = produtoAtualizado.UnidadeMedidaFornecedorMOQ;
        UnidadeMedidaFornecedorPreco = produtoAtualizado.UnidadeMedidaFornecedorPreco;
        UnidadeMedidaBunzl = produtoAtualizado.UnidadeMedidaBunzl;
        QuantidadeMinimaPedido = produtoAtualizado.QuantidadeMinimaPedido;
        Preco = produtoAtualizado.Preco;
        IncotermId = produtoAtualizado.IncotermId;
        TermoPagamento = produtoAtualizado.TermoPagamento;
        Observacoes = produtoAtualizado.Observacoes;
        DetalhesEmbalagem = produtoAtualizado.DetalhesEmbalagem;
        PesoBruto = produtoAtualizado.PesoBruto;
        Comprimento = produtoAtualizado.Comprimento;
        Largura = produtoAtualizado.Largura;
        Altura = produtoAtualizado.Altura;
        TempoEntrega = produtoAtualizado.TempoEntrega;
        CustoDesenvolvimentoEmbalagem = produtoAtualizado.CustoDesenvolvimentoEmbalagem;
        CustoRotulagemEmbalagem = produtoAtualizado.CustoRotulagemEmbalagem;
        PortoEmbarque = produtoAtualizado.PortoEmbarque;
        QuantidadeCarregamentoContainer20Ft = produtoAtualizado.QuantidadeCarregamentoContainer20Ft;
        QuantidadeCarregamentoContainer40Ft = produtoAtualizado.QuantidadeCarregamentoContainer40Ft;
        QuantidadeCarregamentoContainer40Hc = produtoAtualizado.QuantidadeCarregamentoContainer40Hc;
        CodigoArtigo = produtoAtualizado.CodigoArtigo;
        Familia = produtoAtualizado.Familia;
        CodigoSku = produtoAtualizado.CodigoSku;
        CorBunzl = produtoAtualizado.CorBunzl;
        TamanhoBunzl = produtoAtualizado.TamanhoBunzl;
        CBM = CalcularCbm(produtoAtualizado.Comprimento, produtoAtualizado.Largura, produtoAtualizado.Altura);
        Status = produtoAtualizado.Status;
		TipoEmbalagemInterna = produtoAtualizado.TipoEmbalagemInterna;
		QuantidadePorEmbalagemInterna = produtoAtualizado.QuantidadePorEmbalagemInterna;
		TipoCaixaMaster = produtoAtualizado.TipoCaixaMaster;
		QuantidadePorCaixaMaster = produtoAtualizado.QuantidadePorCaixaMaster;
		CapacidadeMensalFabrica = produtoAtualizado.CapacidadeMensalFabrica;
		UnidadeMedidaCapacidadeMensal = produtoAtualizado.UnidadeMedidaCapacidadeMensal;
		NomeFabrica = produtoAtualizado.NomeFabrica;
		CustoDetalhadoMateriaPrima = produtoAtualizado.CustoDetalhadoMateriaPrima;
		CustoDetalhadoCombustivel = produtoAtualizado.CustoDetalhadoCombustivel;
		CustoDetalhadoEmbalagem = produtoAtualizado.CustoDetalhadoEmbalagem;
		CustoDetalhadoMaoDeObra = produtoAtualizado.CustoDetalhadoMaoDeObra;
		CustoDetalhadoEnergia = produtoAtualizado.CustoDetalhadoEnergia;
		CustoDetalhadoTransporte = produtoAtualizado.CustoDetalhadoTransporte;
		AddNotifications(new FornecedorProdutoValidator().Validate(this));
    }

    public void AssociarProdutoBunzl(string? descricao, string? unidadeMedida, string? codigoSKU, string? codigoArtigo, string? familia, string? cor, string? tamanho, EStatusProduto status = EStatusProduto.NaoHomologado)
    {
        DescricaoCompletaBunzl = descricao;
        UnidadeMedidaBunzl = unidadeMedida;
        CodigoSku = codigoSKU;
        CodigoArtigo = codigoArtigo;
        Familia = familia;
        CorBunzl = cor;
        TamanhoBunzl = tamanho;
        Status = status;
        AddNotifications(new FornecedorProdutoValidator().Validate(this));
    }

    public void AdicionarAnexo(FornecedorProdutoAnexo fornecedorProdutoAnexos)
    {
        FornecedorProdutoAnexos.Add(fornecedorProdutoAnexos);
    }

	public void DeletarAnexo(FornecedorProdutoAnexo fornecedorProdutoAnexos)
    {
        FornecedorProdutoAnexos.Remove(fornecedorProdutoAnexos);
    }

    private static decimal CalcularCbm(decimal comprimento, decimal largura, decimal altura)
    {
	    var comprimentoMetros = comprimento / 100;
	    var larguraMetros = largura / 100;
	    var alturaMetros = altura / 100;

	    var volume = comprimentoMetros * larguraMetros * alturaMetros;
	    return volume;
    }
}
