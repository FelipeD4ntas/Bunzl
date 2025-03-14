using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class ProdutoAlteradoModel(string nome, string nomeFornecedor, string produtoCodigo, string produtoDescricao, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.ProdutoAlterado, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public string ProdutoCodigo { get; } = produtoCodigo;
    public string ProdutoDescricao { get; } = produtoDescricao;
}
