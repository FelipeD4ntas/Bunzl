namespace Bunzl.Domain.Commands.Fornecedor.ObterDocumento;

public class FornecedorObterDocumentoResponse(Guid id, string nome, string tipo, string observacao, Guid fornecedorId, DateTime dataCriacao, string? base64 = null)
{
    public Guid Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Tipo { get; set; } = tipo;
    public string? Observacao { get; set; } = observacao;
    public Guid FornecedorId { get; set; } = fornecedorId;
    public DateTime DataCriacao { get; set; } = dataCriacao;
    public string? Base64 { get; set; } = base64;
}