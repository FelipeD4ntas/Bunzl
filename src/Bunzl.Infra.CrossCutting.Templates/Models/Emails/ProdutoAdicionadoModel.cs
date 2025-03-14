using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class ProdutoAdicionadoModel(string nome, string nomeFornecedor, string produtoDescricao, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.ProdutoAdicionado, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string ProdutoDescricao { get; } = produtoDescricao;
}
