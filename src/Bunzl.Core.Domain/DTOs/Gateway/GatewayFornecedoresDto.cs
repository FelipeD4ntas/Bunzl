namespace Bunzl.Core.Domain.DTOs.Gateway;

public class GatewayFornecedoresDto
{
    public string? CodigoFornecedor { get; set; }
    public string? CodigoERP { get; set; }
    public string? Pais { get; set; }
    public string? Empresa { get; set; }
    public string? NomeCompleto { get; set; }
    public string? CodigoNF { get; set; }
    public string? TipoPessoa { get; set; }
    public string? CodigoPessoa { get; set; }
    public string? NomeFantasia { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
    public string? Cidade { get; set; }
    public string? WebSite { get; set; }
    public string? NomeContato { get; set; }
    public string? Email { get; set; }
    public string? TelefoneDDI { get; set; }
    public string? Telefone { get; set; }
    public string? WhatsApp { get; set; }
    public string? Sigla { get; set; }
    public bool IsBloqueado { get; set; } = false;
    public DateTime? DataAlteracao { get; set; }
}
