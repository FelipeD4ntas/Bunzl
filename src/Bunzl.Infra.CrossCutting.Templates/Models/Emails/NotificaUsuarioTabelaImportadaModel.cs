using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NotificaUsuarioTabelaImportadaModel(string usuarioNome, string fornecedorNome, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.NotificaUsuarioTabelaImportada, destinatario, assunto, titulo)
{
    public string UsuarioNome { get; } = usuarioNome;
    public string FornecedorNome { get; } = fornecedorNome;
}