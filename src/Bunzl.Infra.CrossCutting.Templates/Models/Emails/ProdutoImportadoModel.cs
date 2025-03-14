using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class ProdutoImportadoModel(string nome, string nomeFornecedor, int quantidadeProdutos, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.ProdutoImportado, destinatario, assunto, titulo)
{
    public string Nome { get; } = nome;
    public string NomeFornecedor { get; } = nomeFornecedor;
    public int QuantiodadeProdutos { get; } = quantidadeProdutos;
}
