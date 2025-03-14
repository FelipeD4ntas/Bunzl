using Bunzl.Core.Domain.Entities.Base;

namespace Bunzl.Domain.Entities;

public class FornecedorDadoBancario : EntityBase<Guid>
{
    public string? NomeBanco { get; set; }
    public string? NumeroBanco { get; set; }
    public string? Agencia { get; set; }
    public string? NumeroContaCorrente { get; set; }
    public string? NomeBeneficiario { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? ZipCode { get; set; }
    public string? Pais { get; set; }
    public string? EstadoProvincia { get; set; }
    public string? Cidade { get; set; }
    public string? Swift { get; set; }
    public string? Observacao { get; set; }
    public string? Iban { get; set; }
    public string? VatNumber { get; set; }
    public virtual Fornecedor Fornecedor { get; set; }
}