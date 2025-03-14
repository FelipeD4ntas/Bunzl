using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NotificaUsuarioTabelaReprovadaModel(string fornecedorNome, string usuarioNome, string empresaNome, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.NotificaUsuarioTabelaReprovada, destinatario, assunto, titulo)
{
    public string FornecedorNome { get; } = fornecedorNome;
    public string UsuarioNome { get; } = usuarioNome;
    public string EmpresaNome { get; } = empresaNome;
}