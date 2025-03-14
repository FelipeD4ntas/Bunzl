namespace Bunzl.Domain.Commands.Incoterm.Listar;

public class IncotermListarResponse
{
    public Guid Id { get; set; }
    public required string Sigla { get; set; }
    public required string Descricao { get; set; }
}