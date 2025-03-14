using Bunzl.Domain.DTOs.Base;
using Bunzl.Domain.Entities;
using System.Text.Json.Serialization;

namespace Bunzl.Domain.Commands.OrdemDeCompra.AdicionarAnexo;

public class OrdemDeCompraAdicionarAnexoResponse(Guid id, string mensagem, OrdemDeCompraAnexo anexo, Entities.Fornecedor? fornecedor = null, string? numero = null, string? usuarioAutenticadoNome = null, Entities.Empresa? empresa = null, List<Entities.Usuario>? usuarios = null, string? status = null, bool ehPerfilFornecedor = false) : BaseResponseDto(id, mensagem)
{
    [JsonIgnore]
    public OrdemDeCompraAnexo Anexo { get; set; } = anexo;
    [JsonIgnore]
    public Entities.Fornecedor? Fornecedor { get; set; } = fornecedor;

    [JsonIgnore]
    public Entities.Empresa? Empresa { get; set; } = empresa;

    [JsonIgnore]
    public List<Entities.Usuario>? Usuarios { get; set; } = usuarios;

    [JsonIgnore]
    public string? UsuarioAutenticadoNome { get; set; } = usuarioAutenticadoNome;

    [JsonIgnore]
    public string? Numero { get; set; } = numero;

    [JsonIgnore]
    public string? Status { get; set; } = status;

    public bool EhPerfilFornecedor { get; set; } = ehPerfilFornecedor;

    public Guid AnexoId { get; set; }
}