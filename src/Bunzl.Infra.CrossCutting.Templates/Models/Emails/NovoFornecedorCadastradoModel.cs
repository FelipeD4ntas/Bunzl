using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class NovoFornecedorCadastradoModel(string nome, string nomeFornecedor, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.NovoFornecedorCadastrado, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string NomeFornecedor { get; } = nomeFornecedor;
}
