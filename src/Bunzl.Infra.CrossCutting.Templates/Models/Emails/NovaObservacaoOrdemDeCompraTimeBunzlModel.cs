using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NovaObservacaoOrdemDeCompraTimeBunzlModel(string nomeEmpresa, string nomeFornecedor, string usuarioNome, string numeroOrdemDeCompra, string link, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.NovaObservacaoOrdemDeCompraTimeBunzl, destinatario, assunto, titulo)
{
	public string NomeEmpresa { get; } = nomeEmpresa;
	public string NumeroOrdemDeCompra { get; } = numeroOrdemDeCompra;
    public string UsuarioNome { get; } = usuarioNome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string Link { get; } = link;
}

