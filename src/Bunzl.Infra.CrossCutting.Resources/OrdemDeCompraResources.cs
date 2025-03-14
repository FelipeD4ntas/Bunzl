namespace Bunzl.Infra.CrossCutting.Resources;

public struct OrdemDeCompraResources
{
    public const string OrdemDeCompraAdicionadaComSucesso = "ORDEM_DE_COMPRA_ADICIONADA_COM_SUCESSO";
    public const string OrdemDeCompraNaoEncontrada = "ORDEM_DE_COMPRA_NAO_ENCONTRADA";
    public const string ObservacaoAdicionadaComSucesso = "OBSERVACAO_ADICIONADA_COM_SUCESSO";
    public const string ObservacaoNaoEncontrada = "OBSERVACAO_NAO_ENCONTRADA";
    public const string ObservacaoDeletadaComSucesso = "OBSERVACAO_DELETADA_COM_SUCESSO";
    public const string AnexoAdicionadoComSucesso = "ANEXO_ADICIONADO_COM_SUCESSO";
    public const string AnexoNaoEncontrado = "ANEXO_NAO_ENCONTRADO";
    public const string AnexoDeletadoComSucesso = "ANEXO_DELETADO_COM_SUCESSO";
    public const string ObservacaoDeveConterNoMaximo500Caracteres = "OBSERVACAO_DEVE_CONTER_NO_MAXIMO_500_CARACTERES";
    public const string FalhaEnviarEmailNovaObservacao = "FALHA_ENVIAR_EMAIL_NOVA_OBSERVACAO";
    public const string NaoFoiEncontradoOrdensDeCompraNoGateway = "NAO_FOI_ENCONTRADO_ORDENS_DE_COMPRA_NO_GATEWAY";
    public const string UsuarioNaoPodeAlterarStatusDaOrdemDeCompra = "USUARIO_NAO_PODE_ALTERAR_STATUS_DA_ORDEM_DE_COMPRA";
    public const string StatusAtualizadoComSucesso = "STATUS_ATUALIZADO_COM_SUCESSO";
    public const string UsuarioNaoPodeAlterarParaEsseStatus = "USUARIO_NAO_PODE_ALTERAR_PARA_ESSE_STATUS";
    public const string FalhaEnviarEmailStatusAlterado = "FALHA_ENVIAR_EMAIL_STATUS_ALTERADO";
    public const string FalhaEnviarEmailNovoAnexo = "FALHA_ENVIAR_EMAIL_NOVO_ANEXO";
    public const string NaoForamEncontradasOrdensDeCompraParaEsseFornecedor = "NAO_FORAM_ENCONTRADAS_ORDENS_DE_COMPRA_PARA_ESSE_FORNECEDOR";
    public const string OrdemDeCompraSoPodeSerAtualizadaParaAguardandoPi = "ORDEM_DE_COMPRA_SO_PODE_SER_ATUALIZADA_PARA_AGUARDANDO_PI";
}

