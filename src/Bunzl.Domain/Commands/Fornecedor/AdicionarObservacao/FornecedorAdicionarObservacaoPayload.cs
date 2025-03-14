namespace Bunzl.Domain.Commands.Fornecedor.AdicionarObservacao;

public class FornecedorAdicionarObservacaoPayload
{
    public required string Observacao { get; set; }

    public FornecedorAdicionarObservacaoRequest ToRequest(Guid fornecedorId)
    {
        return new FornecedorAdicionarObservacaoRequest(fornecedorId, Observacao);
    }
}
