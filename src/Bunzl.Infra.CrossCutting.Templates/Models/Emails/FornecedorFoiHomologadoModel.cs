using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class FornecedorFoiHomologadoModel(string nomeFornecedor, string link, string destinatario, string nomeEmpresa, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.FornecedorFoiHomologado, destinatario, assunto, titulo)
{
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string Link { get; } = link;
    public string NomeEmpresa = nomeEmpresa;
}