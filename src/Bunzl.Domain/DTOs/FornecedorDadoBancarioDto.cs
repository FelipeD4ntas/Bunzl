namespace Bunzl.Domain.DTOs;

public class FornecedorDadoBancarioDto(
    string nomeBeneficiario,
    string logradouro,
    string numero,
    string zipCode,
    string bairro,
    string pais,
    string estadoProvincia,
    string cidade,
    string swift,
    string observacao,
    string numeroBanco,
    string agencia,
    string numeroContaCorrente,
    string iban,
    string vatNumber,
    string? nomeBanco
)
{
    public string? NomeBanco { get; set; } = nomeBanco;
    public string NumeroBanco { get; set; } = numeroBanco;
    public string Agencia { get; set; } = agencia;
    public string NumeroContaCorrente { get; set; } = numeroContaCorrente;
    public string NomeBeneficiario { get; set; } = nomeBeneficiario;
    public string Logradouro { get; set; } = logradouro;
    public string Numero { get; set; } = numero;
    public string? ZipCode { get; set; } = zipCode;
    public string Bairro { get; set; } = bairro;
    public string Pais { get; set; } = pais;
    public string EstadoProvincia { get; set; } = estadoProvincia;
    public string Cidade { get; set; } = cidade;
    public string? Swift { get; set; } = swift;
    public string? Observacao { get; set; } = observacao;
    public string? Iban { get; set; } = iban;
    public string? VatNumber { get; set; } = vatNumber;
}
