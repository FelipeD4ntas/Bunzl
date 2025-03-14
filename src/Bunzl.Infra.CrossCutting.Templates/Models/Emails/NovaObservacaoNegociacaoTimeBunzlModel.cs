using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NovaObservacaoNegociacaoTimeBunzlModel(string nomeFornecedor, string usuarioNome, string codigoNegociacao, string link, string destinatario, string assunto, string titulo)
	: TemplateEmailModelBase(TipoTemplateEmailEnum.NovaObservacaoNegociacaoTimeBunzl, destinatario, assunto, titulo)
{
	public string CodigoNegociacao { get; } = codigoNegociacao;
	public string UsuarioNome { get; } = usuarioNome;
	public string NomeFornecedor { get; } = nomeFornecedor;
	public string Link { get; } = link;
}
