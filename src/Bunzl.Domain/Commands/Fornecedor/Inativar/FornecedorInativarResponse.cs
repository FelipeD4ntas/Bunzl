namespace Bunzl.Domain.Commands.Fornecedor.Inativar;

public class FornecedorInativarResponse(Guid id)
{
    public Guid Id { get; } = id;
}
