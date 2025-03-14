namespace Bunzl.Domain.Commands.Fornecedor.InvalidarPortal;

public class FornecedorInvalidarPortalResponse(Guid id)
{
    public Guid Id { get; } = id;
}
