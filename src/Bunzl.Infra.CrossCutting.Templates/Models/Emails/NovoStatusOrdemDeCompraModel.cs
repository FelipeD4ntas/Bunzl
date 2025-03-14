using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NovoStatusOrdemDeCompraModel(string nomeFornecedor, string? numeroOrdemDeCompra, string? status, string link, string destinatario, string assunto, string titulo)
	: TemplateEmailModelBase(TipoTemplateEmailEnum.NovoStatusOrdemDeCompra, destinatario, assunto, titulo)
{
	public string? NumeroOrdemDeCompra { get; } = numeroOrdemDeCompra;
	public string NomeFornecedor { get; } = nomeFornecedor;
	public string? Status { get; } = status;
	public string Link { get; } = link;
}
