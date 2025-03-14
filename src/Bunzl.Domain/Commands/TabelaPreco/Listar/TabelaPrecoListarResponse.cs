using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.TabelaPreco.Listar;

public class TabelaPrecoListarResponse
{
    public Guid Id { get; set; }
    public string? CodigoERP { get; set; }
    public DateTime DataInicioVigencia { get; set; }
    public DateTime? DataFimVigencia { get; set; }
    public EStatusTabelaPreco Status { get; set; }
    public bool FlagExpirada { get; set; }
    public Guid UsuarioAlteracao { get; set; }
    public string? UsuarioAlteracaoNome { get; set; }
}
