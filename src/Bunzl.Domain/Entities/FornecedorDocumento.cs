using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class FornecedorDocumento : EntityBase<Guid>
{
    public string Nome { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public Guid FornecedorId { get; set; }
    public virtual Fornecedor? Fornecedor { get; set; }

    public FornecedorDocumento(string nome, string tipo, string? observacao, Guid fornecedorId)
    {
        Nome = nome;
        Tipo = tipo;
        Observacao = observacao;
        FornecedorId = fornecedorId;
    }
}