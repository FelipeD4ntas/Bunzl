namespace Bunzl.Infra.CrossCutting.Resources;

public struct TabelaPrecoResources
{
	public const string TabelaPrecoAdicionaComSucesso = "TABELA_PRECO_ADICIONADA_COM_SUCESSO";
    public const string TabelaPrecoAtualizadaComSucesso = "TABELA_PRECO_ATUALIZADA_COM_SUCESSO";
    public const string TabelaPrecoValidadaComSucesso = "TABELA_PRECO_VALIDADA_COM_SUCESSO";
    public const string TabelaPrecoAprovadaComSucesso = "TABELA_PRECO_APROVADA_COM_SUCESSO";
    public const string TabelaPrecoCanceladaComSucesso = "TABELA_PRECO_CANCELADA_COM_SUCESSO";
    public const string TabelaPrecoNaoEncontrada = "TABELA_PRECO_NAO_ENCONTRADA";
    public const string TabelaPrecoProdutoNaoEncontrado = "TABELA_PRECO_PRODUTO_NAO_ENCONTRADO: {0}";
    public const string TabelaPrecoNaoPodeSerFinalizadaPoisContemProdutosAguardandoAprovacao = "TABELA_PRECO_NAO_PODE_SER_FINALIZADA_POIS_CONTEM_PRODUTOS_AGUARDANDO_APROVACAO";
    public const string TabelaPrecoComStatusDiferenteDeAprovada = "TABELA_PRECO_COM_STATUS_DIFERENTE_DE_APROVADA";

    public const string FalhaEnviarEmailTabelaPrecoImportada = "FALHA_ENVIAR_EMAIL_TABELA_PRECO_IMPORTADA";
    public const string FalhaEnviarEmailTabelaPrecoAtualizada = "FALHA_ENVIAR_EMAIL_TABELA_PRECO_ATUALIZADA";
    public const string FalhaEnviarEmailTabelaPrecoAprovada = "FALHA_ENVIAR_EMAIL_TABELA_PRECO_APROVADA";
}