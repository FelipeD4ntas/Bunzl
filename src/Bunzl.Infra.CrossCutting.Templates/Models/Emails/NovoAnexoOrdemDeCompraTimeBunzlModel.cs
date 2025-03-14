using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NovoAnexoOrdemDeCompraTimeBunzlModel(string nomeEmpresa, string nomeFornecedor, string usuarioNome, string numeroOrdemDeCompra, string status, string link, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.NovoAnexoOrdemDeCompraTimeBunzl, destinatario, assunto, titulo)
{
	public string NomeEmpresa { get; } = nomeEmpresa;
	public string NumeroOrdemDeCompra { get; } = numeroOrdemDeCompra;
    public string UsuarioNome { get; } = usuarioNome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string Status { get; } = status;
    public string Link { get; } = link;
}

