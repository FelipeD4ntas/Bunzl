using Bunzl.Domain.DTOs.TabelaPreco;
using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.TabelaPreco.ObterAguardandoAprovacao;

public class TabelaPrecoObterAguardandoAprovacaoResponse
{
    public Guid Id { get; set; }
    public DateTime DataInicioVigencia { get; set; }
    public DateTime? DataFimVigencia { get; set; }
    public string? CodigoERP { get; set; }
    public EStatusTabelaPreco Status { get; set; }
    public bool FlagExpirada { get; set; }
    public List<TabelaPrecoProdutoObterDto> Produtos { get; set; } = [];
}