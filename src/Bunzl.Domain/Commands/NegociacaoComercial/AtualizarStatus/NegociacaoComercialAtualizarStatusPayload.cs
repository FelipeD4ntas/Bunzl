using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.NegociacaoComercial.AtualizarStatus;

public class NegociacaoComercialAtualizarStatusPayload
{
    public Guid FornecedorId { get; set; }
    public EStatusNegociacaoComercial Status { get; set; }

    public NegociacaoComercialAtualizarStatusRequest ToRequest(Guid id)
    {
        return new NegociacaoComercialAtualizarStatusRequest(id, Status);
    }
}

