using Bunzl.Infra.CrossCutting.Templates.Enums;
using Bunzl.Infra.CrossCutting.Templates.Models.Base;

namespace Bunzl.Infra.CrossCutting.Templates.Models.Emails;

public class SolicitacaoNegociacaoModel(string nomeFornecedor, string nomeEmpresa, string link, string destinatario, string assunto, string titulo)
    : TemplateEmailModelBase(TipoTemplateEmailEnum.SolicitacaoNegociacao, destinatario, assunto, titulo)
{
    public string NomeFornecedor { get; } = nomeFornecedor;
	public string NomeEmpresa { get; } = nomeEmpresa;
	public string Link { get; } = link;
}

