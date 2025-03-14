using Bunzl.Infra.CrossCutting.Templates.Enums;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Base;

public abstract class TemplateEmailModelBase(TipoTemplateEmailEnum tipo, string destinatario, string assunto, string titulo)
{
    public TipoTemplateEmailEnum Tipo { get; } = tipo;
    public string Destinatario { get; } = destinatario;
    public string Assunto { get; } = assunto;
    public string Titulo { get; } = titulo;
}
