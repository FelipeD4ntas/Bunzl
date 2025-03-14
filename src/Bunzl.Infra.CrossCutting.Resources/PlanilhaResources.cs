namespace Bunzl.Infra.CrossCutting.Resources;

public struct PlanilhaResources
{
    public const string ErroConverterInt = "ERRO_CONVERTER_INTEIRO_LINHA_{0}_COLUNA_{1}";
    public const string ErroConverterDecimal = "ERRO_CONVERTER_DECIMAL_LINHA_{0}_COLUNA_{1}";
    public const string CampoVazio = "CAMPO_VAZIO_LINHA_{0}_COLUNA_{1}";
    public const string IncontermInvalidoLinha = "INCONTERM_INVALIDO_LINHA_{0}_COLUNA_{1}";

    public const string CodigoFornecedor = "2";
    public const string DescricaoCompletaFornecedor = "3";
    public const string CodigoNcm = "6";
    public const string QuantidadeEmbalagemInterna = "8";
    public const string QuantidadePorCaixaMaster = "10";
    public const string PesoBruto = "11";
    public const string CustoDesenvolvimentoEmbalagem = "15";
    public const string CustoRotulagemEmbalagem = "16";
    public const string QuantidadeMinimaPedido = "17";
    public const string UnidadeMedidaFornecedorMOQ = "18";
    public const string Preco = "19";
    public const string UnidadeMedidaFornecedorPreco = "20";
    public const string TempoEntrega = "22";
    public const string Incorterm = "23";
    public const string CapacidadeMensalFabrica = "24";
    public const string MateriaPrimaPercentual = "26";
    public const string CombustivelPercentual = "27";
    public const string EmbalagemPercentual = "28";
    public const string MaoDeObraPercentual = "29";
    public const string EnergiaPercentual = "30";
    public const string TransportePercentual = "31";
    public const string Comprimento = "35)";
    public const string Largura = "36";
    public const string Altura = "37";
    public const string QuantidadeCarregamentoContainer20Ft = "38";
    public const string QuantidadeCarregamentoContainer40Ft = "39";
    public const string QuantidadeCarregamentoContainer40Hc = "40";

    public const string CodigoSkuDeveSerPreenchidoLinha = "CODIGO_SKU_DEVE_SER_PREENCHIDO_LINHA_{0}_COLUNA_{1}";
    public const string CodigoProdutoNaoEncontrado = "CODIGO_PRODUTO_NAO_ENCONTRADO_{0}";
    public const string UltimoPrecoPraticado = "Último Preço Praticado";
    public const string NovoPreco = "Novo Preço";
    public const string ColunasPlanilhaInvalidas = "COLUNAS_PLANILHA_INVALIDAS";
}
