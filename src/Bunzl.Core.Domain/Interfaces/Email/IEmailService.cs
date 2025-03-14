
using Bunzl.Core.Domain.DTOs.Email;

namespace Bunzl.Core.Domain.Interfaces.Email;

public interface IEmailService
{
    Task<EnviarEmail> EnviarEmail(string destinatario, string assunto, string corpo, bool corpoHtml);
    Task<EnviarEmail> EnviarEmailUsuarioCadastro(string para, string usuarioNome, Guid chaveCadastro);
    Task<EnviarEmail> EnviarEmailUsuarioJaCadastrado(string para, string usuarioNome, string empresaNome);
    Task<EnviarEmail> EnviarEmailUsuarioResetSenha(string para, string usuarioNome, Guid chaveResetSenha);
    Task<EnviarEmail> EnviarEmailUsuarioCodigoOtp(string para, string usuarioNome, string codigoOtp);
    Task<EnviarEmail> EnviarEmailFornecedorHomologar(string para, string usuarioNome, string fornecedorNome, Guid fornecedorId);
    Task<EnviarEmail> EnviarEmailFornecedorFoiHomologado(string para, string fornecedorNome, string nomeEmpresa);
    Task<EnviarEmail> EnviarEmailNovoFornecedorCadastrado(string para, string usuarioNome, string fornecedorNome);
    Task<EnviarEmail> EnviarEmailProdutoAdicionado(string para, string usuarioNome, string fornecedorNome, string produtoDescricao);
    Task<EnviarEmail> EnviarEmailProdutoAlterado(string para, string usuarioNome, string fornecedorNome, string produtoCodigo, string produtoDescricao);
    Task<EnviarEmail> EnviarEmailProdutoImportado(string para, string usuarioNome, string fornecedorNome, int quantidadeProdutos);
    Task<EnviarEmail> EnviarEmailNovoAnexoProduto(string para, string usuarioNome, string fornecedorNome, string produtoCodigo, string produtoDescricao);
    Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaImportada(string para, string usuarioNome, string fornecedorNome);
    Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaAtualizada(string para, string usuarioNome, string fornecedorNome);
    Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaAprovada(string para, string fornecedorNome, string usuarioNome, string empresaNome);
    Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaReprovada(string para, string fornecedorNome, string usuarioNome, string empresaNome);
    Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaValidada(string para, string fornecedorNome, string usuarioNome, string empresaNome);
    Task<EnviarEmail> EnviarEmailSolicitacaoNegociacao(string para, string fornecedorNome, string empresaNome);
    Task<EnviarEmail> EnviarEmailNovaObservacaoNegociacaoTimeBunzl(string para, string fornecedorNome, string usuarioNome, string codigoNegociacao);
    Task<EnviarEmail> EnviarEmailNovaObservacaoNegociacaoParaFornecedor(string para, string fornecedorNome, string usuarioNome, string codigoNegociacao);
    Task<EnviarEmail> EnviarEmailNovaObservacaoOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome);
    Task<EnviarEmail> EnviarEmailNovaObservacaoOrdemDeCompraParaFornecedor(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome);
    Task<EnviarEmail> EnviarEmailNovaAnexoOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status);
    Task<EnviarEmail> EnviarEmailNovaAnexoStatusOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status);
    Task<EnviarEmail> EnviarEmailNovaAnexoOrdemDeCompraFornecedor(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status);
    Task<EnviarEmail> EnviarEmailNovoStatusOrdemDeCompra(string para, string fornecedorNome, string? numeroOrdemDeCompra, string? status);
}