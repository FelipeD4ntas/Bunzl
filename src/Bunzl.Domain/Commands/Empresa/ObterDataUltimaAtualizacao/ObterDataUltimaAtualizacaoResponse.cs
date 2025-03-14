namespace Bunzl.Domain.Commands.Empresa.ObterDataUltimaAtualizacao;

public class ObterDataUltimaAtualizacaoResponse(Guid id, string nome, DateTime? dataUltimaAtualizacao, DateTime? dataUltimaAtualizacaoOrdemDeCompra)
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public DateTime? DataUltimaAtualizacao { get; set; } = dataUltimaAtualizacao;
    public DateTime? DataUltimaAtualizacaoOrdemDeCompra { get; set; } = dataUltimaAtualizacaoOrdemDeCompra;
}
