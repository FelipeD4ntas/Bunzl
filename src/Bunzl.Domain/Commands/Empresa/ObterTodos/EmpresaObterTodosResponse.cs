namespace Bunzl.Domain.Commands.Empresa.ObterTodos;

public class EmpresaObterTodosResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
    public DateTime? DataUltimaAtualizacaoOrdemDeCompra { get; set; }
}