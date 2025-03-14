using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class FornecedorHomologarModel(string nome, string link, string nomeFornecedor, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.FornecedorHomologar, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string Link { get; } = link;
}
