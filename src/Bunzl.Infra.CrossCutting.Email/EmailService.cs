using Bunzl.Core.Domain.DTOs.Email;
using Bunzl.Core.Domain.Interfaces.Email;
using Bunzl.Infra.CrossCutting.IoC.Interfaces;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;
using Bunzl.Infra.CrossCutting.Templates.Models.Emails;
using Bunzl.Infra.CrossCutting.Templates.Services.Interfaces;
using Bunzl.Infra.CrossCutting.Templates.Utils;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Bunzl.Infra.CrossCutting.Email;

public class EmailService : IEmailService, IInjectScoped
{
    private readonly string _linkBase;
    private readonly string _de;
    private readonly string _nomeExibicao;
    private readonly string _smtpHost;
    private readonly string _userName;
    private readonly string _senha;
    private readonly int _smtpPorta;
    private readonly string _domain;
    private readonly bool _enableSsl;
    private IRazorViewToStringRenderer _razorViewToStringRenderer;

    public EmailService(IConfiguration configuration, IRazorViewToStringRenderer razorViewToStringRenderer)
    {
        _linkBase = configuration["LinkBase"]!;
        _de = configuration["MailSettings:From"]!;
        _nomeExibicao = configuration["MailSettings:DisplayName"]!;
        _smtpHost = configuration["MailSettings:Host"]!;
        _userName = configuration["MailSettings:UserName"]!;
        _senha = configuration["MailSettings:Password"]!;
        _smtpPorta = Convert.ToInt32(configuration["MailSettings:Port"]!);
        _domain = configuration["MailSettings:Domain"]!;
        _enableSsl = Convert.ToBoolean(configuration["MailSettings:EnableSsl"]!);

        _razorViewToStringRenderer = razorViewToStringRenderer;
    }

    public async Task<EnviarEmail> EnviarEmail(string destinatario, string assunto, string corpo, bool corpoHtml)
    {
        try
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_de, _nomeExibicao)
            };

            mail.To.Add(new MailAddress(destinatario));

            mail.Subject = _nomeExibicao + " Soft - " + assunto;
            mail.Body = corpo;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            //outras opções
            //mail.Attachments.Add(new Attachment(arquivo));

            using var smtp = new SmtpClient(_smtpHost, _smtpPorta);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_userName, _senha, _domain);
            smtp.EnableSsl = _enableSsl;
            smtp.Send(mail);

            return await Task.FromResult(
                new EnviarEmail
                {
                    Sucesso = true,
                    Mensagem = "Email enviado com sucesso!!!"
                });
        }
        catch (Exception ex)
        {
            return await Task.FromResult(
                new EnviarEmail
                {
                    Sucesso = false,
                    Mensagem = ex.InnerException == null ? "Erro ao enviar o e-mail: " + ex.Message : "Erro ao enviar o e-mail: " + ex.Message + " - " + ex.InnerException.Message
                });
        }
    }

    public async Task<EnviarEmail> EnviarEmailUsuarioCadastro(string para, string usuarioNome, Guid chaveCadastro)
    {
        var linkCompleto = $"{_linkBase}/usuario/concluir-cadastro/{chaveCadastro}/{usuarioNome}";
        var model = new UsuarioCadastroModel(usuarioNome, linkCompleto, para, "User Registration", "Importers Portal");
        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailUsuarioJaCadastrado(string para, string usuarioNome, string empresaNome)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new UsuarioJaCadastradoModel(usuarioNome, empresaNome, linkCompleto, para, "Registered User", "Importers Portal");
        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailUsuarioResetSenha(string para, string usuarioNome, Guid chaveResetSenha)
    {
        var linkCompleto = $"{_linkBase}/nova-senha/{chaveResetSenha}";
        var model = new UsuarioResetSenhaModel(usuarioNome, linkCompleto, para, "User Password Reset", "Importers Portal");
        return await EnviarEmailModel(model);
    }
        
    public async Task<EnviarEmail> EnviarEmailUsuarioCodigoOtp(string para, string usuarioNome, string codigoOtp)
    {
        var model = new UsuarioCodigoOtpModel(usuarioNome, codigoOtp, para, "User Access Code", "Importers Portal");
        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailFornecedorHomologar(string para, string usuarioNome, string fornecedorNome, Guid fornecedorId)
    {
        var redirectUrl = Uri.EscapeDataString($"/fornecedor/editar/{fornecedorId}");
        var linkCompleto = $"{_linkBase}/check-login?redirect={redirectUrl}";
        var model = new FornecedorHomologarModel(usuarioNome, linkCompleto, fornecedorNome, para, "Supplier Info Update", "Importers Portal");
        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailFornecedorFoiHomologado(string para, string fornecedorNome, string nomeEmpresa)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new FornecedorFoiHomologadoModel(fornecedorNome, linkCompleto, para, nomeEmpresa, "Bunzl has approved your registration ", "Importers Portal");
        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNovoFornecedorCadastrado(string para, string usuarioNome, string nomeFornecedor)
    {
        var model = new NovoFornecedorCadastradoModel(usuarioNome, nomeFornecedor, para, "New Supplier Registered", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailProdutoAdicionado(string para, string usuarioNome, string fornecedorNome, string produtoDescricao)
    {
        var model = new ProdutoAdicionadoModel(usuarioNome, fornecedorNome, produtoDescricao, para, "New Product", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailProdutoAlterado(string para, string usuarioNome, string fornecedorNome, string produtoCodigo, string produtoDescricao)
    {
       var model = new ProdutoAlteradoModel(usuarioNome, fornecedorNome, produtoCodigo, produtoDescricao, para, "Supplier updated its product", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailProdutoImportado(string para, string usuarioNome, string fornecedorNome, int quantidadeProdutos)
    {
        var model = new ProdutoImportadoModel(usuarioNome, fornecedorNome, quantidadeProdutos, para, "Supplier added a range of products", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNovoAnexoProduto(string para, string usuarioNome, string fornecedorNome, string produtoCodigo, string produtoDescricao)
    {
        var model = new NovoAnexoProdutoModel(usuarioNome, fornecedorNome, produtoCodigo, produtoDescricao, para, "New Attachment has been added to the product ", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaImportada(string para, string usuarioNome, string fornecedorNome)
    {
        var model = new NotificaUsuarioTabelaImportadaModel(usuarioNome, fornecedorNome, para, "New price list added", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaAtualizada(string para, string usuarioNome, string fornecedorNome)
    {
        var model = new NotificaUsuarioTabelaAtualizadaModel(usuarioNome, fornecedorNome, para, "Tabela Atualizada", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaAprovada(string para, string fornecedorNome, string usuarioNome, string empresaNome)
    {
        var model = new NotificaUsuarioTabelaAprovadaModel(fornecedorNome, usuarioNome, empresaNome, para, "New price approved", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaReprovada(string para, string fornecedorNome, string usuarioNome, string empresaNome)
    {
        var model = new NotificaUsuarioTabelaReprovadaModel(fornecedorNome, usuarioNome, empresaNome, para, "New price list refused", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public async Task<EnviarEmail> EnviarEmailNotificaUsuarioTabelaValidada(string para, string fornecedorNome, string usuarioNome, string empresaNome)
    {
        var model = new NotificaUsuarioTabelaReprovadaModel(fornecedorNome, usuarioNome, empresaNome, para, "New price list refused", "Importers Portal");

        return await EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailSolicitacaoNegociacao(string para, string fornecedorNome, string empresaNome)
    {
	    var linkCompleto = $"{_linkBase}/login";
		var model = new SolicitacaoNegociacaoModel(fornecedorNome, empresaNome, linkCompleto, para, "Commercial Negotiation Request", "Importers Portal");
        
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovaObservacaoNegociacaoTimeBunzl(string para, string fornecedorNome, string usuarioNome, string codigoNegociacao)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new NovaObservacaoNegociacaoTimeBunzlModel(fornecedorNome, usuarioNome, codigoNegociacao, linkCompleto, para, "New Message Commercial Negotiation", "Importers Portal");
		return EnviarEmailModel(model);
	}

    public Task<EnviarEmail> EnviarEmailNovaObservacaoNegociacaoParaFornecedor(string para, string fornecedorNome, string usuarioNome, string codigoNegociacao)
    {
        var linkCompleto = $"{_linkBase}/login";
		var model = new NovaObservacaoNegociacaoFornecedorModel(fornecedorNome, usuarioNome, codigoNegociacao, linkCompleto, para, "New Message Commercial Negotiation", "Importers Portal");
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovaObservacaoOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new NovaObservacaoOrdemDeCompraTimeBunzlModel(empresaNome, fornecedorNome, usuarioNome, numeroOrdemDeCompra, linkCompleto, para, "New Purchase Order Message", "Importers Portal");
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovaObservacaoOrdemDeCompraParaFornecedor(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new NovaObservacaoOrdemDeCompraFornecedorModel(empresaNome, fornecedorNome, usuarioNome, numeroOrdemDeCompra, linkCompleto, para, "New Purchase Order Message", "Importers Portal");
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovaAnexoOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status)
    {
		var linkCompleto = $"{_linkBase}/login";
		var model = new NovoAnexoOrdemDeCompraTimeBunzlModel(empresaNome, fornecedorNome, usuarioNome, numeroOrdemDeCompra, status, linkCompleto, para, "New Purchase Order Attachment", "Importers Portal");
		return EnviarEmailModel(model);
	}
    public Task<EnviarEmail> EnviarEmailNovaAnexoStatusOrdemDeCompraTimeBunzl(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new NovoAnexoStatusOrdemDeCompraTimeBunzlModel(empresaNome, fornecedorNome, usuarioNome, numeroOrdemDeCompra, status, linkCompleto, para, "Purchase Order Status Modified", "Importers Portal");
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovaAnexoOrdemDeCompraFornecedor(string para, string fornecedorNome, string usuarioNome, string numeroOrdemDeCompra, string empresaNome, string status)
    {
        var linkCompleto = $"{_linkBase}/login";
        var model = new NovoAnexoOrdemDeCompraFornecedorModel(empresaNome, fornecedorNome, usuarioNome, numeroOrdemDeCompra, status, linkCompleto, para, "New Purchase Order Attachment", "Importers Portal");
        return EnviarEmailModel(model);
    }

    public Task<EnviarEmail> EnviarEmailNovoStatusOrdemDeCompra(string para, string fornecedorNome, string? numeroOrdemDeCompra, string? status)
    {
		var linkCompleto = $"{_linkBase}/login";
		var model = new NovoStatusOrdemDeCompraModel(fornecedorNome, numeroOrdemDeCompra, status, linkCompleto, para, "Purchase Order Status Has Been Modified", "Importers Portal");
		return EnviarEmailModel(model);
	}

    private async Task<EnviarEmail> EnviarEmailModel<T>(T model)
        where T : TemplateEmailModelBase
    {
        var corpo = await _razorViewToStringRenderer.RenderViewToStringAsync(TemplatePathsUtil.ObterPathEmail(model.Tipo.ToString()), model);
        return await EnviarEmail(model.Destinatario, model.Assunto, corpo, true);
    }
}