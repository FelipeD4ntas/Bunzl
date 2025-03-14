namespace Bunzl.Domain.Commands.Moeda.Listar;

public class MoedaListarResponse
{
    public Guid Id { get; set; }
    public required string Sigla { get; set; }
    public required string Descricao { get; set; }
}