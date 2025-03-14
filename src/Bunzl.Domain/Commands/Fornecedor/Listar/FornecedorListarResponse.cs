using Bunzl.Domain.Enumerators;

namespace Bunzl.Domain.Commands.Fornecedor.Listar;

public class FornecedorListarResponse
{
    public Guid Id { get; set; }
    public required string RazaoSocial { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string? ZipCode { get; set; }
    public string Bairro { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public string EstadoProvincia { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string? NumeroIdentificacaoFiscal { get; set; }
    public string? CodigoERP { get; set; } = string.Empty;
    public DateTime? DataFabricaAuditada { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public EStatusFornecedor Status { get; set; }
    public bool FlagBloqueadoErp { get; set; }
}
