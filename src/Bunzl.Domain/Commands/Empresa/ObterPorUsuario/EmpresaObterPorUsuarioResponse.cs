namespace Bunzl.Domain.Commands.Empresa.ObterPorUsuario;

public class EmpresaObterPorUsuarioResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public Guid? FornecedorLogadoId { get; set; }
	public DateTime? DataUltimaAtualizacao { get; set; }
    public DateTime? DataUltimaAtualizacaoOrdemDeCompra { get; set; }
}