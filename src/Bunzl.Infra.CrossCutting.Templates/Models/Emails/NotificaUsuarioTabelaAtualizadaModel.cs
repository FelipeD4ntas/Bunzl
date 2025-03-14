using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NotificaUsuarioTabelaAtualizadaModel(string usuarioNome, string fornecedorNome, string destinatario, string assunto, string titulo)
	: TemplateEmailModelBase(TipoTemplateEmailEnum.NotificaUsuarioTabelaAtualizada, destinatario, assunto, titulo)
{
	public string UsuarioNome { get; } = usuarioNome;
	public string FornecedorNome { get; } = fornecedorNome;
}
