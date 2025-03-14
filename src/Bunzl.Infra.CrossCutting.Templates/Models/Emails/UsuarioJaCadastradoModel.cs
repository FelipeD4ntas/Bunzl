using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class UsuarioJaCadastradoModel(string nome, string empresaNome, string link, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.UsuarioJaCadastrado, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string EmpresaNome { get; } = empresaNome;
    public string Link { get; } = link;
}
